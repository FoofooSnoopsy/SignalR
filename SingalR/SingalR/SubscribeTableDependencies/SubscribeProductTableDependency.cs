using SingalR.Hubs;
using SingalR.Models;
using TableDependency.SqlClient;

namespace SingalR.SubscribeTableDependencies
{
    public class SubscribeProductTableDependency:ISubsribeTableDependency
    {
        SqlTableDependency<Product> _tableDependency;
        SqlTableDependency<Sale> _tableDependencySale;
        DashboardHub _dashboardHub;
        IConfiguration _cofiguration;

        public SubscribeProductTableDependency(DashboardHub dashboardHub, IConfiguration configuration)
        {
            _dashboardHub = dashboardHub;
            _cofiguration = configuration;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            var productDependency = new SqlTableDependency<Product>(connectionString);
            var saleDependency = new SqlTableDependency<Sale>(connectionString);

            productDependency.OnChanged += TableDependency_Onchanged;
            productDependency.OnError += TableDependency_OnError;
            productDependency.Start();

            saleDependency.OnChanged += TableDependency_OnchangedSale;
            saleDependency.OnError += TableDependency_OnErrorSale;
            saleDependency.Start();
        }
        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Product)} SqlTableDependency error: {e.Error.Message}");
        }

        private void TableDependency_Onchanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Product> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                _dashboardHub.SendProducts();
            }
        }

        private void TableDependency_OnErrorSale(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(Sale)} SqlTableDependency error: {e.Error.Message}");
        }

        private void TableDependency_OnchangedSale(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Sale> e)
        {
            if (e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                _dashboardHub.SendSales();
            }
        }
    }
}
