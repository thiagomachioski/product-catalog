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
        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<StoreDataContext>(options =>
                options
                    .UseSqlServer(connectionString, sqlOptions => {
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