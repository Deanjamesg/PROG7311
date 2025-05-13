using AECPrototype.Models;
using AECPrototype.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AECPrototype.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //private readonly UserService _userService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserController(ILogger<HomeController> logger, UserManager<User> _userManager, SignInManager<User> _signInManager)
        {
            _logger = logger;
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User registered successfully: {Email}", model.Email);

                    await userManager.AddToRoleAsync(user, model.Role);

                    return View(model);
                }
                else
                {
                    foreach (var error in result.Errors.Where(e => e.Code != "DuplicateUserName"))
                    {
                        if (error == result.Errors.FirstOrDefault(e => e.Code == "DuplicateEmail"))
                        {
                            ModelState.AddModelError("Email", "This email is already taken.");
                        }
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Profile");
                }
                else
                {
                    ModelState.AddModelError("Email", "Invalid email or password.");
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Profile()
        {
            if (signInManager.IsSignedIn(User))
            {
                var user = await userManager.GetUserAsync(User);

                if (user != null)
                {
                    var roles = await userManager.GetRolesAsync(user);

                    UserViewModel model = new UserViewModel
                    {
                        UserId = user.Id.ToString(),
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Password = user.PasswordHash,
                        Role = roles.ToList()
                    };
                    return View(model);
                }
            }

            return RedirectToAction("Login");

        }
    }
}
