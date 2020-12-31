using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Products.Catalog.Domain.Products;
using Products.Catalog.Repository.Data;

namespace Products.Catalog.Repository.Products
{
    public class ProductRepository: IProductRepository
    {

        private readonly StoreDataContext _context;

        public ProductRepository(StoreDataContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> Get()
        {
            return _context.Products
                    .Include(x => x.Category)
                   .AsNoTracking()
                   .ToList();
        }
        
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
    }
}
