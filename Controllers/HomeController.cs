using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TrackIt.Models;

namespace TrackIt.Controllers
{
    public class HomeController : Controller
    {
        // Injecting the logger service for logging purposes
        private readonly ILogger<HomeController> _logger;

        // Constructor with dependency injection for logger
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Action method for the default home page (Index.cshtml)
        public IActionResult Index()
        {
            return View();
        }

        // Action method for the privacy page (Privacy.cshtml)
        public IActionResult Privacy()
        {
            return View();
        }

        // Handles unexpected errors and shows the Error view
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Creates an ErrorViewModel with the request ID for diagnostics
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
