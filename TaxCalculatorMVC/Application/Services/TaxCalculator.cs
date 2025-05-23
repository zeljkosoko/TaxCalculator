using TaxCalculatorMVC.Application.Enums;
using TaxCalculatorMVC.Application.Interfaces;

namespace TaxCalculatorMVC.Application.Services
{
    public class TaxCalculator : ITaxCalculator
    {
        private readonly IStandardRateProvider _standardProvider;
        private readonly ICustomRateRepository _customRepo;

        public TaxCalculator(IStandardRateProvider standardRateProvider, ICustomRateRepository customRateRepository)
        {
            _customRepo = customRateRepository;
            _standardProvider = standardRateProvider;
        }

        public double GetStandardTaxRate(Commodity commodity) => 
            _standardProvider.GetRate(commodity);

        public void SetCustomTaxRate(Commodity commodity, double rate)
        {
            var nowUtc = DateTime.UtcNow;
            _customRepo.SaveRate(commodity, rate, nowUtc);
        }

        public double GetTaxRateForDateTime(Commodity commodity, DateTime date)
        {
            var utc = date.ToUniversalTime();
            var rates = _customRepo.GetRates(commodity)
                .Where(r => r.TimestampUtc <= utc)
                .OrderBy(r => r.TimestampUtc)
                .LastOrDefault();

            return rates != default ? rates.Rate : _standardProvider.GetRate(commodity);
        }

        public double GetCurrentTaxRate(Commodity commodity) => 
            GetTaxRateForDateTime(commodity, DateTime.UtcNow);

    }
}
