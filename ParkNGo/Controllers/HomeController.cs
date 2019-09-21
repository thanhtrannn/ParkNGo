using Microsoft.AspNetCore.Mvc;
using ParkNGo.Models;
using System.Diagnostics;

namespace ParkNGo.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult GetViewComponent(string view)
        {
            return ViewComponent("Registration", view);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string id)
        {
            return RedirectToAction("Main");
        }
        public IActionResult Main()
        {
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Forgot()
        {
            return View();
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
