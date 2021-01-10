using System.Collections.Generic;

namespace Products.Catalog.Domain.Categories
{
    public interface ICategoryRepository
    {
        IList<Category> Get();
        Category GetById(int id);
        void Save(Category category);
        void Update(Category category);
        Category Reload(Category category);
    }
}