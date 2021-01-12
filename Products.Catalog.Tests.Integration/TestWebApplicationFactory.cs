using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.Catalog.Repository.BlobStorage;
using Products.Catalog.Repository.Data;
using Products.Catalog.Tests.Integration.Generators;
using Products.Catalog.Tests.Integration.Mock;
using Products.Catalog.UI;

namespace Products.Catalog.Tests.Integration
{
    public class TestWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddTransient<IBlobStorageProvider, BlobStorageMockProvider>();
                services.AddTransient<ICategoryGenerator, CategoryGenerator>();
                services.AddTransient<IProductGenerator, ProductGenerator>();

                var descriptor = services.SingleOrDefault(e => 
                    e.ServiceType == typeof(DbContextOptions<StoreDataContext>)
                );
                
                if (descriptor != null)
                    services.Remove(descriptor);

                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<StoreDataContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryTestDatabase");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                using var scope = services.BuildServiceProvider().CreateScope();
                using var dbContext = scope.ServiceProvider.GetRequiredService<StoreDataContext>();

                dbContext.Database.EnsureCreated();

            }).UseEnvironment("IntegrationTest");
        }
    }
}