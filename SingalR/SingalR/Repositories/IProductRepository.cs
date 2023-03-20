using SingalR.Models;
using SingalR.Models.ViewModels;

namespace SingalR.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProductDetails(int productId);

        public Task<List<ProductGraphData>> GetProductGraphData();
    }
}
