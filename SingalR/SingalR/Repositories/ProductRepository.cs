using Microsoft.EntityFrameworkCore;
using SingalR.Data;
using SingalR.Models;
using SingalR.Models.ViewModels;

namespace SingalR.Repositories
{
    public class ProductRepository : IRepository<Product, ProductGraphData>
    {
        public readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            return await _db.Product.AsNoTracking().ToListAsync();
        }

        public async Task<List<ProductGraphData>> GetItemGraphData()
        {
            var category = await _db.Product.GroupBy(p => p.Category).Select(p => new
            {
                Category = p.Key,
                Count = p.Count()
            }).OrderBy(p => p.Count).ToListAsync();

            return category.Select(item => new ProductGraphData
            {
                Category = item.Category,
                Count = item.Count
            }).ToList();
        }

        public Task<Product> GetItemDetails(int itemId)
        {
            return _db.Product.FirstOrDefaultAsync(p => p.Id == itemId)!;
        }
    }
}
