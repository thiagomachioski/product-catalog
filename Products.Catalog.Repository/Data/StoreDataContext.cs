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
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public StoreDataContext(DbContextOptions<StoreDataContext> options): 
            base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new CategoryMap());
        }
    }
}
