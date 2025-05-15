using AECPrototype.Models;
using AECPrototype.Services;
using AECPrototype.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AECPrototype.Controllers
{
    public class ProductController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ProductService productService;

        public ProductController(UserManager<User> _userManager, SignInManager<User> _signInManager, ProductService _productService)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            productService = _productService;
        }

        [Authorize(Roles = "Farmer")]
        public IActionResult UserProduct()
        {
            return View();
        }

        [Authorize(Roles = "Farmer")]
        public IActionResult AddProduct()
        {
            return View();
        }

        [Authorize(Roles = "Employee")]
        public IActionResult SearchProducts()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Farmer")]
        public async Task<IActionResult> AddProduct(AddProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product
                {
                    Title = model.Title,
                    Category = model.Category,
                    Date = model.Date,
                    UserId = userManager.GetUserId(User)
                };
                await productService.CreateProductAsync(product);

                return RedirectToAction("UserProduct");
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Farmer")]
        public async Task<IActionResult> UserProduct(UserProductViewModel model)
        {
            var userId = userManager.GetUserId(User);

            var products = await productService.GetProductsByUserIdAsync(userId);

            var viewModel = products.Select(p => new UserProductViewModel
            {
                ProductId = p.ProductId.ToString(),
                Title = p.Title,
                Category = p.Category,
                Date = p.Date
            }).ToList();

            return View(viewModel);
        }


        [Authorize(Roles = "Employee")]
        [HttpGet]
        public async Task<IActionResult> SearchProducts(FilterProductViewModel model)
        {
            var categories = await productService.GetAllProductCategoriesAsync();

            var farmers = await userManager.GetUsersInRoleAsync("Farmer");

            List<FarmerViewModel> farmerList = new List<FarmerViewModel>();

            if (farmers.Any())
            {
                foreach (var user in farmers)
                {
                    var farmerVM = new FarmerViewModel
                    {
                        FarmerName = user.FirstName + " " + user.LastName,
                        FarmerId = user.Id
                    };
                    farmerList.Add(farmerVM);
                }
            }

            if (farmerList.Count() != 0 && farmerList != null)
            {
                model.Farmers = farmerList;
            }

            if (categories != null)
            {
                model.Categories = categories.ToList();
                return View(model);
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> FilterProduct(FilterProductViewModel model)
        {
            if (model.FromDate != null && model.ToDate == null)
            {
                ModelState.AddModelError("ToDate", "Select a date.");
            }
            else if (model.FromDate == null && model.ToDate != null)
            {
                ModelState.AddModelError("FromDate", "Select a date.");
            }
            else
            {
                var filteredProducts = await productService.GetFilteredProductsAsync(model);
                if (filteredProducts.Count != 0)
                {
                    foreach (var product in filteredProducts)
                    {
                        var productVM = new ProductViewModel
                        {
                            Id = product.ProductId.ToString(),
                            Title = product.Title,
                            Category = product.Category,
                            FarmerName = product.User.FirstName + " " + product.User.LastName,
                            Date = product.Date,
                        };
                        model.Products.Add(productVM);
                    }
                }
            }

            var categories = await productService.GetAllProductCategoriesAsync();

            var farmers = await userManager.GetUsersInRoleAsync("Farmer");

            List<FarmerViewModel> farmerList = new List<FarmerViewModel>();

            if (farmers.Any())
            {
                foreach (var user in farmers)
                {
                    var farmerVM = new FarmerViewModel
                    {
                        FarmerName = user.FirstName + " " + user.LastName,
                        FarmerId = user.Id
                    };
                    farmerList.Add(farmerVM);
                }
            }

            if (farmerList.Count() != 0 && farmerList != null)
            {
                model.Farmers = farmerList;
            }

            if (categories != null)
            {
                model.Categories = categories.ToList();
            }

            return View("SearchProducts", model);
        }

    }
}
