using System.Collections.Generic;
using Products.Catalog.Domain.Products;

namespace Products.Catalog.Domain.Categories
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
