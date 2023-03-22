using SingalR.Data;
using SingalR.Models.ViewModels;
using SingalR.Models;
using Microsoft.EntityFrameworkCore;
using Nest;

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
            return _db.Sale
            .AsNoTracking()
            .GroupBy(s => s.PurchasedOn)
            .Select(g => new SaleGraphData
            {
                Day = g.Key,
                Count = g.Count(),
            })
            .ToList();
        }

    }
}

