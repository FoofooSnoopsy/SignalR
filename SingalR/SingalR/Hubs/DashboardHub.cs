using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.SignalR;
using SingalR.Models;
using SingalR.Models.ViewModels;
using SingalR.Repositories;

namespace SingalR.Hubs
{
    public class DashboardHub : Hub
    {
        private readonly IRepository<Product, ProductGraphData> _productRepository;
        private readonly IRepository<Sale, SaleGraphData> _saleRepository;
        private readonly IRepository<Customer, CustomerGraphData> _customerRepository;
        public DashboardHub(
            IRepository<Product, ProductGraphData> productRepository,
            IRepository<Sale, SaleGraphData> saleRepository,
            IRepository<Customer, CustomerGraphData> customerRepository)
        {
            _productRepository = productRepository;
            _saleRepository = saleRepository;
            _customerRepository = customerRepository;
        }

        public async Task SendProducts()
        {
            var products = await _productRepository.GetItems();
            await Clients.All.SendAsync("ReceivedProducts", products);

            var graphData = await _productRepository.GetItemGraphData();
            await Clients.All.SendAsync("ReceivedProductsGraphData", graphData);
        }
        public async Task SendSales()
        {
            var sales = await _saleRepository.GetItems();
            await Clients.All.SendAsync("ReceivedSales", sales);

            var saleGraphData = await _saleRepository.GetItemGraphData();
            await Clients.All.SendAsync("ReceivedSalesGraphData", saleGraphData);
        }

        public async Task SendCustomers()
        {
            var customers = await _customerRepository.GetItems();
            await Clients.All.SendAsync("ReceivedCustomers", customers);

            var customerGraphData = await _customerRepository.GetItemGraphData();
            await Clients.All.SendAsync("ReceivedCustomersGraphData", customerGraphData);
        }

    }
}
