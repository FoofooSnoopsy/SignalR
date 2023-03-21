using SignalR2.Models;

namespace SignalR2.Repositories
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAll();
    }
}
