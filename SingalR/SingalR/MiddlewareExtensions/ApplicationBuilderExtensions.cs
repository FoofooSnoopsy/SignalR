using SingalR.SubscribeProductTableDependencies;

namespace SingalR.MiddlewareExtensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseProductTableDependency(this IApplicationBuilder appbuilder, string connectionString)
        {
            var serviceProvider = appbuilder.ApplicationServices;
            var service = serviceProvider.GetService<SubscribeProductTableDependency>();
            service!.SubscribeTableDependency(connectionString);
        }
    }
}
