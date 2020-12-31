using Microsoft.EntityFrameworkCore;
using Products.Catalog.Domain.Categories;
using Products.Catalog.Domain.Products;
using Products.Catalog.Repository.Categories;
using Products.Catalog.Repository.Products;

namespace Products.Catalog.Repository.Data
{
    public class StoreDataContext : DbContext
    {
        private const string ConnectionString = "Server=localhost;Database=ProductCatalogDb;User Id=sa;Password=vxz9fR/$qD6A/4x-]ASaCs+$[^;";
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new CategoryMap());
        }
    }
}
