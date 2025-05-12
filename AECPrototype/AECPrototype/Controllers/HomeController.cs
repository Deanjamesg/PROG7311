using System.Diagnostics;
using AECPrototype.Models;
using AECPrototype.Services;
using Microsoft.AspNetCore.Mvc;

namespace AECPrototype.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UserService _userService;

        private static Stopwatch _stopwatch = new Stopwatch();

        public HomeController(ILogger<HomeController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.AddAsync(user);
                return RedirectToAction("Index", user);
            }
            return View("Index");
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            _stopwatch.Restart(); // Start or reset the stopwatch

            var users = await _userService.GetAllAsync();

            _stopwatch.Stop(); // Stop the stopwatch

            // Log the elapsed time
            _logger.LogInformation($"Time: {_stopwatch.ElapsedMilliseconds} ms");

            return View(users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
