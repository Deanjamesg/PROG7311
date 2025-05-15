using AECPrototype.Data;
using AECPrototype.Models;
using AECPrototype.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace AECPrototype.Services
{
    public class ProductService
    {
        private readonly AppDbContext context;

        public ProductService(AppDbContext _context)
        {
            context = _context;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            context.Products.Add(product);

            var result = await context.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> GetProductsByUserIdAsync(string userId)
        {
            return await context.Products
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<string>> GetAllProductCategoriesAsync()
        {
            return await context.Products
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<Product>> GetFilteredProductsAsync(FilterProductViewModel filters)
        {
            IQueryable<Product> query = context.Products.Include(p => p.User);
            if (!string.IsNullOrEmpty(filters.SelectedCategory))
            {
                query = query.Where(p => p.Category == filters.SelectedCategory);
            }
            if (!string.IsNullOrEmpty(filters.SelectedFarmer))
            {
                query = query.Where(p => p.UserId == filters.SelectedFarmer);
            }
            if (filters.FromDate.HasValue)
            {
                query = query.Where(p => p.Date >= filters.FromDate.Value);
            }
            if (filters.ToDate.HasValue)
            {
                query = query.Where(p => p.Date <= filters.ToDate.Value);
            }
            query = query.OrderByDescending(p => p.Date);

            return await query.ToListAsync();
        }
    }
}