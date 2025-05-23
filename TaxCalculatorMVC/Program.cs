using TaxCalculatorMVC.Application.Interfaces;
using TaxCalculatorMVC.Application.Services;
using TaxCalculatorMVC.Infrastructure.InMemory;
using TaxCalculatorMVC.Infrastructure.Standard;
using TaxCalculatorMVC.Infrastructure;

namespace TaxCalculatorMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Service registration grouping by extension method
            builder.Services.AddTaxCalculatorServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Tax}/{action=Index}/{id?}");

            app.Run();
        }
    }
}