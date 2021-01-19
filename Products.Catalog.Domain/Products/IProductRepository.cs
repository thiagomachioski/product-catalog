using System.Collections.Generic;

namespace Products.Catalog.Domain.Products
{
    public interface IProductRepository
    {
        IList<Product> Get(int page, int itemsPerPage, string query);
        Product GetById(int id);
        void Save(Product product);
        void Update(Product product);
        Product Reload(Product product);
    }
}