using Microsoft.AspNetCore.Mvc;
using TaxCalculatorMVC.Application.Interfaces;

namespace TaxCalculatorMVC.Controllers
{
    public class TaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
