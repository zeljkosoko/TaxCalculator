using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace TaxCalculatorMVC.Tests
{
    public class TaxIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TaxIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetRateForDate_ReturnsExpectedHtml()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/Tax/GetRateForDate?commodity=Alcohol&date=2025-05-23T16:00:42Z");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("Alcohol", html);
            Assert.Contains("%", html);
        }
    }
}
