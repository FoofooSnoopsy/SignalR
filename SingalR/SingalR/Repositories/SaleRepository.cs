using SingalR.Models.ViewModels;
using SingalR.Models;
using SingalR.Data;
using Microsoft.EntityFrameworkCore;

namespace SingalR.Repositories
{
    public class SaleRepository : IRepository<Sale, SaleGraphData>
    {
        private readonly ApplicationDbContext _context;

        public SaleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Sale> GetFromId(int productId)
        {
            return await _context.Sale.AsNoTracking().FirstOrDefaultAsync(s => s.Id == productId);
        }

        public async Task<List<SaleGraphData>> GetGraphData()
        {
            return _context.Sale
                .AsNoTracking()
                .GroupBy(s => s.PurchasedOn)
                .Select(g => new SaleGraphData
                {
                    Day = g.Key,
                    Count = g.Count(),
                })
                .ToList();
        }

        public async Task<IEnumerable<Sale>> GetList()
        {
            return await _context.Sale.AsNoTracking().Include(s => s.Customer).ToListAsync();
        }
    }
}
