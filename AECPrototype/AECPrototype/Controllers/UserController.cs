using Microsoft.AspNetCore.Mvc;

namespace AECPrototype.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Profile()
        {
            // If User is logged in
            // Else redirectToAction(Login);
            return View();
        }
    }
}
