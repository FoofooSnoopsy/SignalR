using SingalR.Models;

namespace SingalR.Repositories
{
    public interface IProductRepository
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProductDetails(int productId);
    }
}
