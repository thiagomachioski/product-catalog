using Products.Catalog.Domain.Products;
namespace Products.Catalog.Tests.Integration.Generators
{
    public class ProductGenerator : IProductGenerator
    {
        public Product GenerateAndSave(IProductRepository productRepository, int categoryId)
        {
            var product = new Product
            {
                Title = "Product Title",
                Description = "Product description",
                Price = 150,
                Quantity = 10,
                CategoryId = categoryId
            };
            productRepository.Save(product);

            return product;
        }
    }
}
