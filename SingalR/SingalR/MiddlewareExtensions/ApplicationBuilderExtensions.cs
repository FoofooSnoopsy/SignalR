using SingalR.SubscribeTableDependencies;

namespace SingalR.MiddlewareExtensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseProductTableDependency(this IApplicationBuilder app, string connectionString)
        {
            var serviceProvider = app.ApplicationServices;
            var service = serviceProvider.GetService<SubscribeProductTableDependency>();
            service!.SubscribeTableDependency(connectionString);
        }
    }
}
