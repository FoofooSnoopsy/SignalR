using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SingalR.Data;
using SingalR.Models;
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
            var products = await _productRepository.GetAllProducts();
            await Clients.All.SendAsync("pannekoek", products);

            var graphData = await _productRepository.GetProductGraphData();
            await Clients.All.SendAsync("GraphData", graphData);
        }

    }
}
