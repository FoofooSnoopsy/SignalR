using Microsoft.AspNetCore.SignalR;
using SingalR.Repositories;

namespace SingalR.Hubs
{
    public class DashboardHub : Hub
    {
        private readonly IProductRepository _productRepository;
        public DashboardHub(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task SendProducts()
        {
            var products = await _productRepository.GetProducts();
            await Clients.All.SendAsync("ReceivedProducts", products);
        }
    }
}
