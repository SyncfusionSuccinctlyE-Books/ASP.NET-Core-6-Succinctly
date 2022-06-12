using Microsoft.AspNetCore.Mvc;
//using EssentialCSharp10.Helpers;

namespace EssentialCSharp10.Controllers
{
    public class InfoController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public InfoController(ILogger<HomeController> logger)
        {
            _logger = logger;

            var strValue = "This is a string";
            var sanitizedString = CommonHelperMethods.SanitizeString(strValue);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
