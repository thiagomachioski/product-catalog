using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Products.Catalog.Tests.Integration.Generators;
using Products.Catalog.Tests.Integration.Testes.Common;
using Products.Catalog.UI.Categories.Dtos;
using Xunit;
using Xunit.Abstractions;

namespace Products.Catalog.Tests.Integration.Testes
{
    public class CategoryTest : BaseTest
    {
        private CategoryCommand Command { get; }
        
        public CategoryTest(
            TestWebApplicationFactory factory, 
            ITestOutputHelper output
        ) : base(factory, output)
        {
            Command = new CategoryCommand
            {
                Title = "Category"
            };
        }
        
        [Fact]
        public async Task Title_Cannot_Be_Empty()
        {
            Command.Title = string.Empty;
            var response = await HttpClient.PostAsJsonAsync($"{BasePath}Categories", Command);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Title_Cannot_Be_Null()
        {
            Command.Title = null;
            var response = await HttpClient.PostAsJsonAsync($"{BasePath}Categories", Command);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [Fact]
        public async Task Must_Update()
        {
            var category = CategoryGenerator.GenerateAndSave(CategoryRepository);
            Command.Id = category.Id;
            
            var response = await HttpClient.PutAsJsonAsync($"{BasePath}Categories", Command);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            category = CategoryRepository.Reload(category);
            Assert.Equal(Command.Id, category.Id);
            Assert.Equal(Command.Title, category.Title);
        }
        
        [Fact]
        public async Task Must_Save()
        {
            var response = await HttpClient.PostAsJsonAsync($"{BasePath}Categories", Command);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}