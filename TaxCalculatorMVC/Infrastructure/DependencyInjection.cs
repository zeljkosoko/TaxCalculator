using TaxCalculatorMVC.Application.Interfaces;
using TaxCalculatorMVC.Application.Services;
using TaxCalculatorMVC.Infrastructure.InMemory;
using TaxCalculatorMVC.Infrastructure.Standard;

namespace TaxCalculatorMVC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTaxCalculatorServices(this IServiceCollection services)
        {
            services.AddScoped<ITaxCalculator, TaxCalculator>();
            services.AddSingleton<IStandardRateProvider, StandardRateProvider>();
            services.AddSingleton<ICustomRateRepository, InMemoryCustomRateRepository>();

            return services;
        }
    }
}
