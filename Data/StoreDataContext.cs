using Microsoft.EntityFrameworkCore;
using Products.Catalog.Data.Maps;
using Products.Catalog.Models;

namespace Products.Catalog.Data
{
    public class StoreDataContext : DbContext
    {
        private const string ConnectionString = "Password=!Ban4na359;Persist Security Info=True;User ID=sa;Initial Catalog=firstef;Data Source=localhost,1433";
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
