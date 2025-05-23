using TaxCalculatorMVC.Application.Enums;

namespace TaxCalculatorMVC.Application.Interfaces
{
    public interface ITaxCalculator
    {
        double GetStandardTaxRate(Commodity commodity);
        void SetCustomTaxRate(Commodity commodity, double rate);
        double GetTaxRateForDateTime(Commodity commodity, DateTime date);
        double GetCurrentTaxRate(Commodity commodity);
    }
}
