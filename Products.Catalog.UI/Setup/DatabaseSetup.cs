using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Products.Catalog.Repository.Data;

namespace Products.Catalog.UI.Setup
{
    public static class DatabaseSetup
    {
        private const string ConnectionString = "Password=CoffeAndChill13;Persist Security Info=True;User ID=root;Initial Catalog=ProductCatalog;Data Source=localhost\\SQLEXPRESS";

        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<StoreDataContext>(options =>
                options
                    .UseSqlServer(ConnectionString, sqlOptions => {
                        sqlOptions.EnableRetryOnFailure();
                        sqlOptions.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
                    }).LogTo(e =>
                            Debug.WriteLine(e),                         
                        new[] { DbLoggerCategory.Database.Name },   
                        LogLevel.Information)                       
                    .EnableSensitiveDataLogging()
            );

            return services;
        }
    }
}