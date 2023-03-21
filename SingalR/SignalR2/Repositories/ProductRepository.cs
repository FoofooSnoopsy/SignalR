using Microsoft.EntityFrameworkCore;
using SignalR2.Data;
using SignalR2.Models;

namespace SignalR2.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll()
        {
            var result = await _context.Products.AsNoTracking().ToListAsync();
            return result;
        }
    }
}
