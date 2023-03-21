using SingalR.Models;
using SingalR.ViewModels;

namespace SingalR.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetAllProducts();

        public Task<Product> GetById(int id);

        public Task<List<ProductGraphData>> GetProductGraphData();
    }
}
