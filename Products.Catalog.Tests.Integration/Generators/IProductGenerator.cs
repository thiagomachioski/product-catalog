using Products.Catalog.Domain.Products;

namespace Products.Catalog.Tests.Integration.Generators
{
    public interface IProductGenerator
    {
        Product GenerateAndSave(IProductRepository productRepository);
    }
}