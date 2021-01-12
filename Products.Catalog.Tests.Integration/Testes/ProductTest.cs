using Products.Catalog.Tests.Integration.Generators;
using Products.Catalog.Tests.Integration.Testes.Common;
using Products.Catalog.UI.Products.Dtos;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Products.Catalog.Tests.Integration.Testes
{
    public class ProductTest : BaseTest
    {
        private ProductCreateCommand CreateCommand { get; }
        public ProductTest
            (
            TestWebApplicationFactory factory, 
            ITestOutputHelper output
            ) : base(factory, output)
        {
            CreateCommand = new ProductCreateCommand
            {
                Title = "Product Title",
                Description = "Product description",
                Price = 150,
                Quantity = 10
            };
        }

        [Fact]
        public async Task Must_Save()
        {
            var category = CategoryGenerator.GenerateAndSave(CategoryRepository);
            CreateCommand.CategoryId = category.Id;

            var response = await HttpClient.PostAsJsonAsync($"{BasePath}Products", CreateCommand);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Must_Update()
        {
            var category = CategoryGenerator.GenerateAndSave(CategoryRepository);
            CreateCommand.CategoryId = category.Id;

            var product = ProductGenerator.GenerateAndSave(ProductRepository, category.Id);
            CreateCommand.Id = product.Id;

            var response = await HttpClient.PutAsJsonAsync($"{BasePath}Products", CreateCommand);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            product = ProductRepository.Reload(product);
            Assert.Equal(CreateCommand.Id, product.Id);
            Assert.Equal(CreateCommand.Title, product.Title);
        }

    }
}
