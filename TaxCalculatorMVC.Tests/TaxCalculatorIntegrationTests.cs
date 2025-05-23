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
    public class TaxCalculatorIntegrationTests
    {
        private readonly ITaxCalculator _calculator;

        public TaxCalculatorIntegrationTests()
        {
            var standard = new StandardRateProvider();
            var custom = new InMemoryCustomRateRepository();

            _calculator = new TaxCalculator(standard, custom);
        }

        [Fact]
        public void ShouldReturnLatestCustomRate()
        {
            // Arrange
            _calculator.SetCustomTaxRate(Commodity.Alcohol, 0.68);
            Thread.Sleep(1000); 
            _calculator.SetCustomTaxRate(Commodity.Alcohol, 0.69);
            Thread.Sleep(1000);
            _calculator.SetCustomTaxRate(Commodity.Alcohol, 0.70);

            var now = DateTime.UtcNow;

            // Act
            var rate = _calculator.GetTaxRateForDateTime(Commodity.Alcohol, now);

            // Assert
            Assert.Equal(0.70, rate, precision: 2);
        }
    }
}
