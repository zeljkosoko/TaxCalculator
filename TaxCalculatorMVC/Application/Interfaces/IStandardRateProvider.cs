using TaxCalculatorMVC.Application.Enums;

namespace TaxCalculatorMVC.Application.Interfaces
{
    public interface IStandardRateProvider
    {
        double GetRate(Commodity commodity);
    }
}
