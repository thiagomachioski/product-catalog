using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Products.Catalog.Domain.Products;
using Products.Catalog.Repository.Data;

namespace Products.Catalog.Repository.Products
{
    public class ProductRepository : IProductRepository
    {

        private readonly StoreDataContext _context;

        public ProductRepository(StoreDataContext context)
        {
            _context = context;
        }

        public IList<Product> Get(int page = 1, int itemsPerPage = 10, string query = "") =>
            _context.Products
                .Include(x => x.Category)
                .Where(x => x.Title.Contains(query) || string.IsNullOrEmpty(query))
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();

        public Product GetById(int id)
        {
            return _context.Products
                .Include(e => e.Category)
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefault();
        }
        public void Save(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }
        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Product Reload(Product product)
        {
            _context.Entry(product).Reload();
            return product;
        }

    }
}
