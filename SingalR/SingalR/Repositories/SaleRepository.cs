using SingalR.Data;
using SingalR.Models.ViewModels;
using SingalR.Models;
using Microsoft.EntityFrameworkCore;

namespace SingalR.Repositories
{
    public class SaleRepository : IRepository<Sale, SaleGraphData>
    {
        public readonly ApplicationDbContext _db;

        public SaleRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<Sale> GetItemDetails(int itemId)
        {
            return _db.Sale.FirstOrDefaultAsync(p => p.Id == itemId)!;
        }

        public async Task<IEnumerable<Sale>> GetItems()
        {
            return await _db.Sale.AsNoTracking().ToListAsync();
        }


        public async Task<List<SaleGraphData>> GetItemGraphData()
        {
            var category = await _db.Product.GroupBy(p => p.Category).Select(p => new
            {
                Category = p.Key,
                Count = p.Count()
            }).OrderBy(p => p.Count).ToListAsync();

            return category.Select(item => new SaleGraphData
            {
                Category = item.Category,
                Count = item.Count
            }).ToList();
        }


    }
}

