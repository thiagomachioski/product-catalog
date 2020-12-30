using Microsoft.EntityFrameworkCore;
using Products.Catalog.Data.Maps;
using Products.Catalog.Models;

namespace Products.Catalog.Data
{
    public class StoreDataContext : DbContext
    {
        private const string ConnectionString = "Password=banana;Persist Security Info=True;User ID=root;Initial Catalog=firstef;Data Source=localhost\\SQLEXPRESS";
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
