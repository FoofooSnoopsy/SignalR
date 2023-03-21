using Microsoft.AspNetCore.SignalR;
using SignalR2.Models;
using SignalR2.Repositories;

namespace SignalR2.Hubs
{
    public class DashboardHub : Hub
    {
        private IProductRepository _productRepository;

        public DashboardHub(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async void SendProducts()
        {
            List<Product> products = await _productRepository.GetAll();
            Clients.All.SendAsync("GetProducts", products);
        }
    }
}
