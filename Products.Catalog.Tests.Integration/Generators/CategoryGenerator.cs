using Products.Catalog.Domain.Categories;

namespace Products.Catalog.Tests.Integration.Generators
{
    public class CategoryGenerator : ICategoryGenerator
    {
        public Category GenerateAndSave(ICategoryRepository categoryRepository)
        {
            var category = new Category
            {
                Title = "My category"
            };
            
            categoryRepository.Save(category);
            
            return category;
        }
    }
}