using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculatorMVC.Application.Enums;
using TaxCalculatorMVC.Application.Interfaces;
using TaxCalculatorMVC.Application.Services;
using TaxCalculatorMVC.Infrastructure.InMemory;
using TaxCalculatorMVC.Infrastructure.Standard;

namespace TaxCalculatorMVC.Tests
{
    public class TaxCalculatorTests
    {
        private readonly IStandardRateProvider _standardProvider;
        private readonly ICustomRateRepository _customRepo;
        private readonly ITaxCalculator _taxCalculator;

        public TaxCalculatorTests()
        {
            _standardProvider = new StandardRateProvider();
            _customRepo = new InMemoryCustomRateRepository();
            _taxCalculator = new TaxCalculator(_standardProvider, _customRepo);
        }

        //Standard rate when there are no custom ones.
        [Fact]
        public void Should_Return_Standard_Rate_When_No_Custom_Rates()
        {
            var rate = _taxCalculator.GetCurrentTaxRate(Commodity.Literature);
            Assert.Equal(0.06, rate, 3);
        }

        //The last rate entered for GetCurrent Tax Rate
        [Fact]
        public void Should_Return_Latest_Custom_Rate()
        {
            var commodity = Commodity.Transport;

            _taxCalculator.SetCustomTaxRate(commodity, 0.10);
            Thread.Sleep(1000);
            _taxCalculator.SetCustomTaxRate(commodity, 0.11);
            Thread.Sleep(1000);
            _taxCalculator.SetCustomTaxRate(commodity, 0.12);

            var rate = _taxCalculator.GetCurrentTaxRate(commodity);

            Assert.Equal(0.12, rate, 3);
        }

        //Rate at a specific time by checking the exact behavior of the time range.
        [Fact]
        public void Should_Return_Rate_That_Was_Active_At_Specific_Date()
        {
            var commodity = Commodity.Food;

            var t1 = DateTime.UtcNow;
            _taxCalculator.SetCustomTaxRate(commodity, 0.07);
            Thread.Sleep(1000);
            var t2 = DateTime.UtcNow;
            _taxCalculator.SetCustomTaxRate(commodity, 0.08);
            Thread.Sleep(1000);
            var t3 = DateTime.UtcNow;
            _taxCalculator.SetCustomTaxRate(commodity, 0.09);

            var rate1 = _taxCalculator.GetTaxRateForDateTime(commodity, t1.AddMilliseconds(10));
            var rate2 = _taxCalculator.GetTaxRateForDateTime(commodity, t2.AddMilliseconds(10));
            var rate3 = _taxCalculator.GetTaxRateForDateTime(commodity, t3.AddMilliseconds(10));

            Assert.Equal(0.07, rate1, 3);
            Assert.Equal(0.08, rate2, 3);
            Assert.Equal(0.09, rate3, 3);
        }

        //Fallback to standard rate if the query is for a time before the entered rates.
        [Fact]
        public void Should_Fallback_To_Standard_If_Date_Before_Custom()
        {
            var commodity = Commodity.CulturalServices;

            Thread.Sleep(500);
            _taxCalculator.SetCustomTaxRate(commodity, 0.03);

            var oldDate = DateTime.UtcNow.AddMinutes(-5);
            var rate = _taxCalculator.GetTaxRateForDateTime(commodity, oldDate);

            Assert.Equal(0.06, rate, 3);
        }
    }
}
