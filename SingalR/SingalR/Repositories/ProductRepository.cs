using Microsoft.EntityFrameworkCore;
using SingalR.Data;
using SingalR.Models;

namespace SingalR.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _db.Product.ToListAsync();
        }

        public Task<Product> GetProductDetails(int ProductId)
        {
            return _db.Product.FirstOrDefaultAsync(p => p.Id == ProductId)!;
        }
    }
}
