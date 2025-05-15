using AECPrototype.Models;
using AECPrototype.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AECPrototype.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> logger;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserController(ILogger<UserController> _logger, UserManager<User> _userManager, SignInManager<User> _signInManager)
        {
            logger = _logger;
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public IActionResult UnauthorizedAccess()
        {
            return View();
        }

        [Authorize(Roles = "Employee")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Employee")]
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
                    await userManager.AddToRoleAsync(user, model.Role);

                    return RedirectToAction("Create");
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
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Profile");
                }
                else
                {
                    ViewBag.FailedMessage = "Invalid email or password.";
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Farmer, Employee")]
        public async Task<IActionResult> Profile()
        {
            var user = await userManager.GetUserAsync(User);

            var role = (await userManager.GetRolesAsync(user)).FirstOrDefault();

            UserViewModel model = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = role
            };

            ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;

            return View(model);
        }

    }
}
