using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Products.Catalog.Domain.Categories;
using Products.Catalog.Repository.Data;

namespace Products.Catalog.Repository.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreDataContext _context;

        public CategoryRepository(StoreDataContext context)
        {
            _context = context;
        }

        public IList<Category> Get() => 
            _context.Categories
                .Include(x => x.Products)
                .AsNoTracking()
                .ToList();

        public Category GetById(int id) => 
            _context.Categories
                .Include(x => x.Products)
                .FirstOrDefault(e => e.Id == id);

        public void Save(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public Category Reload(Category category)
        {
            _context.Entry(category).Reload();
            return category;
        }
    }
}
