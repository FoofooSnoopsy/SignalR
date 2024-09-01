using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SingalR.Data;
using SingalR.Hubs;
using SingalR.MiddlewareExtensions;
using SingalR.Models.ViewModels;
using SingalR.Models;
using SingalR.Repositories;
using SingalR.SubscribeTableDependencies;

namespace SingalR
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options => 
            {
                options.UseSqlServer(connectionString);
            }, ServiceLifetime.Singleton);
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddSingleton<IRepository<Product, ProductGraphData>, ProductRepository>();
            builder.Services.AddSingleton<IRepository<Sale, SaleGraphData>, SaleRepository>();
            builder.Services.AddSingleton<IRepository<Customer, CustomerGraphData>, CustomerRepository>();
            builder.Services.AddSingleton<DashboardHub>();
            builder.Services.AddSingleton<SubscribeProductTableDependency>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddSignalR();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Dashboard}/{action=Index}/{id?}");

            app!.UseProductTableDependency(connectionString);

            app.MapRazorPages();

            app.MapHub<DashboardHub>("/dashboardHub");


            app.Run();
        }
    }
}