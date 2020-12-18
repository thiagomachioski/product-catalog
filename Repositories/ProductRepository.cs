using Microsoft.EntityFrameworkCore;
using Products.Catalog.Data;
using Products.Catalog.Models;
using Products.Catalog.ViewModels.ProductViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Products.Catalog.Repositories
{
    public class ProductRepository
    {

        private readonly StoreDataContext _context;

        public ProductRepository(StoreDataContext context)
        {
            _context = context;
        }
        public IEnumerable<ListProductViewModel> Get()
        {
            return _context.Products.Include(x => x.Category)
                   .Select(x => new ListProductViewModel
                   {
                       Id = x.Id,
                       Title = x.Title,
                       Price = x.Price,
                       Category = x.Category.Title,
                       CategoryId = x.Category.Id
                   })
                   .AsNoTracking()
                   .ToList();
        }
        public Product Get(int id)
        {
            return _context.Products.Find(id);
        }
        public void Save(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }
        public void Update(Product product)
        {
            _context.Entry<Product>(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

    }
}
