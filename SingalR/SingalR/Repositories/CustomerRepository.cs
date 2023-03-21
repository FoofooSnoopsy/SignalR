using SingalR.Models.ViewModels;
using SingalR.Models;
using SingalR.Data;
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

        public async Task<Customer> GetFromId(int productId)
        {
            return await _db.Customer.FirstOrDefaultAsync(c => c.Id == productId);
        }

        public async Task<List<CustomerGraphData>> GetGraphData()
        {
            return _db.Customer
                .AsNoTracking()
                .GroupBy(c => c.Gender)
                .Select(g => new CustomerGraphData
                {
                    Gender = g.Key,
                    Count = g.Count()
                })
                .ToList();
        }

        public async Task<IEnumerable<Customer>> GetList()
        {
            return await _db.Customer.ToListAsync();
        }
    }
}
