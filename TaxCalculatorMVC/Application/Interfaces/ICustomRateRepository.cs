using TaxCalculatorMVC.Application.Enums;

namespace TaxCalculatorMVC.Application.Interfaces
{
    public interface ICustomRateRepository
    {
        void SaveRate(Commodity commodity, double rate, DateTime timestampUtc);
        List<(DateTime TimestampUtc, double Rate)> GetRates(Commodity commodity);
    }
}
