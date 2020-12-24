using Microsoft.EntityFrameworkCore;
using Products.Catalog.Data;
using Products.Catalog.Models;
using Products.Catalog.ViewModels.CategoryViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Products.Catalog.Repositories
{
    public class CategoryRepository
    {
        private readonly StoreDataContext _context;

        public CategoryRepository(StoreDataContext context)
        {
            _context = context;
        }

        public IEnumerable<ListCategoryViewModel> Get()
        {
            return _context.Categories
                .Include(x => x.Products)
                .Select(x => new ListCategoryViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Products = x.Products.Select(p => new ListProductForCategoryViewModel
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        Price = p.Price,
                        Quantity = p.Quantity
                    })

                })
                .AsNoTracking()
                .ToList();
        }

        public Category Get(int id)
        {
            return _context.Categories.Find(id);
        }

        public ListCategoryViewModel GetById(int id)
        {
            return _context.Categories
                .Include(x => x.Products)
                .Select(x => new ListCategoryViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Products = x.Products.Select(p => new ListProductForCategoryViewModel
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        Price = p.Price,
                        Quantity = p.Quantity
                    })

                })
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public void Save(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Entry<Category>(category).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
