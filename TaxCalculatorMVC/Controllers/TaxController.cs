using Microsoft.AspNetCore.Mvc;
using TaxCalculatorMVC.Application.Enums;
using TaxCalculatorMVC.Application.Interfaces;

namespace TaxCalculatorMVC.Controllers
{
    public class TaxController : Controller
    {
        private readonly ITaxCalculator _taxCalculator;

        public TaxController(ITaxCalculator taxCalculator)
        {
            _taxCalculator = taxCalculator;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetCustomRate(Commodity commodity, double rate) 
        {
            _taxCalculator.SetCustomTaxRate(commodity, rate);
            ViewBag.Message = $"A tax rate {rate:P0} has been set for the {commodity}";
            return View("Index");
        }

        [HttpGet]
        public IActionResult GetCurrentRate(Commodity commodity) 
        {
            var rate = _taxCalculator.GetCurrentTaxRate(commodity);
            ViewBag.Message = $"Current tax rate za {commodity}: {rate:P0}";
            return View("Index");
        }

        [HttpGet]
        public IActionResult GetRateForDate(Commodity commodity, DateTime date) 
        {
            var timestamp = DateTime.UtcNow;
            var rate = _taxCalculator.GetTaxRateForDateTime(Commodity.Alcohol, timestamp);
            Console.WriteLine($"GET: {commodity} @ {timestamp} = {rate}");

            ViewBag.Message = $"A tax rate for {commodity} at {date:u}: {rate:P0}";
            return View("Index");
        }
    }
}
