using Microsoft.EntityFrameworkCore;
using SingalR.Data;
using SingalR.Models;
using SingalR.ViewModels;

namespace SingalR.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var result = await _context.Product.AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Product.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ProductGraphData>> GetProductGraphData()
        {
            var result = await _context.Product
                .AsNoTracking()
                .GroupBy(p => p.Category)
                .Select(p => new
                {
                    Category = p.Key,
                    Count = p.Count()
                })
                .OrderBy(p => p.Count)
                .ToListAsync();

            return result.Select(p => new ProductGraphData
            {
                Category = p.Category,
                Count = p.Count
            }).ToList();
        }
    }
}
