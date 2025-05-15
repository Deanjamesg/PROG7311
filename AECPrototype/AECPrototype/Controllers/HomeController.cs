using System.Diagnostics;
using AECPrototype.Models;
using AECPrototype.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AECPrototype.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly SignInManager<User> signInManager;

        public HomeController(ILogger<HomeController> _logger, SignInManager<User> _signInManager)
        {
            logger = _logger;
            signInManager = _signInManager;
        }

        public IActionResult Index(LoginViewModel user)
        {
            ViewBag.IsSignedIn = signInManager.IsSignedIn(User);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
