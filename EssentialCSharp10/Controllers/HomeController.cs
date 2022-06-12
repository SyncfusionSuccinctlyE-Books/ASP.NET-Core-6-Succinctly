using EssentialCSharp10.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
//using EssentialCSharp10.Helpers;

namespace EssentialCSharp10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            var strValue = "This is a string";
            var sanitizedString = CommonHelperMethods.SanitizeString(strValue);
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}