using TaxCalculatorMVC.Application.Enums;
using TaxCalculatorMVC.Application.Interfaces;

namespace TaxCalculatorMVC.Infrastructure.Standard
{
    public class StandardRateProvider: IStandardRateProvider
    {
        private static readonly Dictionary<Commodity, double> _rates = new()
        {
            { Commodity.Default, 0.25 },
            { Commodity.Alcohol, 0.25 },
            { Commodity.Food, 0.12 },
            { Commodity.FoodServices, 0.12 },
            { Commodity.Literature, 0.06 },
            { Commodity.Transport, 0.06 },
            { Commodity.CulturalServices, 0.06 }
        };

        public double GetRate(Commodity commodity) =>
            _rates.TryGetValue(commodity, out var rate) ? rate : _rates[Commodity.Default];
    }
}
