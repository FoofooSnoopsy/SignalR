using SingalR.Data;
using SingalR.Models.ViewModels;
using SingalR.Models;
using Microsoft.EntityFrameworkCore;

namespace SingalR.Repositories
{
    public class CustomerRepository : IRepository<Customer, CustomerGraphData>
    {
        public readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<Customer> GetItemDetails(int ItemId)
        {
            return _db.Customer.FirstOrDefaultAsync(p => p.Id == ItemId)!;
        }

        public async Task<IEnumerable<Customer>> GetItems()
        {
            return await _db.Customer.AsNoTracking().ToListAsync();
        }


        public async Task<List<CustomerGraphData>> GetItemGraphData()
        {
            return _db.Customer
             .AsNoTracking()
             .GroupBy(s => s.Gender)
             .Select(g => new CustomerGraphData
             {
                 Gender = g.Key,
                 Count = g.Count(),
             })
             .ToList();
        }
    }
}
