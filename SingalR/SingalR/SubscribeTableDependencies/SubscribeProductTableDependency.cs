using SingalR.Hubs;
using SingalR.Models;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;

namespace SingalR.SubscribeProductTableDependencies
{
    public class SubscribeProductTableDependency : ISubscribeTableDependencies
    {
        SqlTableDependency<Product> _tableDependency;
        DashboardHub _dashboardHub;
        IConfiguration _configuration;


        public SubscribeProductTableDependency(DashboardHub dashboardHub, IConfiguration configuration)
        {
            _dashboardHub = dashboardHub;
            _configuration = configuration;
        }

        public void SubscribeTableDependency(string connectionString)
        {
            _tableDependency = new SqlTableDependency<Product>(connectionString);
            _tableDependency.OnChanged += TableDependency_OnChanged;
            _tableDependency.OnError += TableDependency_OnError;
            _tableDependency.Start();
        }



        private void TableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Product> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                _dashboardHub.SendProducts();
            }
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine("Sql Skill Issue");
        }
    }
}
