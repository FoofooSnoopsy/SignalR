using Microsoft.AspNetCore.SignalR;
using SingalR.Models;
using SingalR.Models.ViewModels;
using SingalR.Repositories;

namespace SingalR.Hubs
{
    public class DashboardHub : Hub
    {
        private readonly ProductRepository _productRepository;

        private readonly SaleRepository _saleRepository;

        private readonly CustomerRepository _customerRepository;

        public DashboardHub(
            ProductRepository productRepository, 
            SaleRepository saleRepository, 
            CustomerRepository customerRepository            
        ) {
            _productRepository = productRepository;
            _saleRepository = saleRepository;
            _customerRepository = customerRepository;
        }



        public async Task SendProducts()
        {
            var products = await _productRepository.GetList();
            await Clients.All.SendAsync("ReceivedProducts", products);

            var graphData = await _productRepository.GetGraphData();
            await Clients.All.SendAsync("ReceivedProductsGraphData", graphData);
        }

        public async Task SendSales()
        {
            var Sales = await _saleRepository.GetList();
            await Clients.All.SendAsync("ReceivedSales", Sales);

            var graphData = await _saleRepository.GetGraphData();
            await Clients.All.SendAsync("ReceivedSalesGraphData", graphData);
        }

        public async Task SendCustomers()
        {
            var Customers = await _customerRepository.GetList();
            await Clients.All.SendAsync("ReceivedCustomers", Customers);

            var graphData = await _customerRepository.GetGraphData();
            await Clients.All.SendAsync("ReceivedCustomersGraphData", graphData);
        }
    }
}
