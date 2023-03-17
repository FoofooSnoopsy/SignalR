using SingalR.Hubs;
using SingalR.Models;
using TableDependency.SqlClient;

namespace SingalR.SubscribeTableDependencies
{
    public class SubscribeProductTableDependency:ISubsribeTableDependency
    {
        SqlTableDependency<Product> _tableDependency;
        DashboardHub _dashboardHub;
        IConfiguration _cofiguration;

        public SubscribeProductTableDependency(DashboardHub dashboardHub, IConfiguration configuration)
        {
            _dashboardHub = dashboardHub;
            _cofiguration = configuration;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            _tableDependency = new SqlTableDependency<Product>(connectionString);
            _tableDependency.OnChanged += TableDependency_Onchanged;
            _tableDependency.OnError += TableDependency_OnError;
            _tableDependency.Start();
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
    }
}
