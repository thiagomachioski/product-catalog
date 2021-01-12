using Products.Catalog.Domain.Categories;

namespace Products.Catalog.Tests.Integration.Generators
{
    public interface ICategoryGenerator
    {
        Category GenerateAndSave(ICategoryRepository categoryRepository);
    }
}
