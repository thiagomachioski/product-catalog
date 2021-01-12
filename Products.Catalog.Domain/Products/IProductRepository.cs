using System.Collections.Generic;

namespace Products.Catalog.Domain.Products
{
    public interface IProductRepository
    {
        IEnumerable<Product> Get();
        Product GetById(int id);
        void Save(Product product);
        void Update(Product product);
        Product Reload(Product product);
    }
}