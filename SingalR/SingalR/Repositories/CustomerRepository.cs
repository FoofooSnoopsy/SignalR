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
            var category = await _db.Product.GroupBy(p => p.Category).Select(p => new
            {
                Category = p.Key,
                Count = p.Count()
            }).OrderBy(p => p.Count).ToListAsync();

            return category.Select(item => new CustomerGraphData
            {
                Category = item.Category,
                Count = item.Count
            }).ToList();
        }
    }
}
