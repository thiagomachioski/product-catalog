using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Products.Catalog.Domain.Categories;
using Products.Catalog.Domain.Products;
using Products.Catalog.Repository.Categories;
using Products.Catalog.Repository.Products;
using System.Diagnostics;

namespace Products.Catalog.Repository.Data
{
    public class StoreDataContext : DbContext
    {
        private const string ConnectionString = "Password=CoffeAndChill13;Persist Security Info=True;User ID=root;Initial Catalog=ProductCatalog;Data Source=localhost\\SQLEXPRESS";
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString)
                .LogTo(e =>
                    Debug.WriteLine(e),                         //Target delegate
                    new[] { DbLoggerCategory.Database.Name },   //Filter log messages
                    LogLevel.Information)                       //Control log level
                .EnableSensitiveDataLogging();                  //Show parameters. Watch out for sensitive data on production environment;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new CategoryMap());
        }
    }
}
