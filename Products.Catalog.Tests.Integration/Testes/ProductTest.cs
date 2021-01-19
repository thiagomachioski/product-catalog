using Bogus;
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
        private ProductCreateCommand Command { get; }
        private Faker Faker { get; set; } = new Faker();
        public ProductTest
            (
            TestWebApplicationFactory factory,
            ITestOutputHelper output
            ) : base(factory, output)
        {
            var category = CategoryGenerator.GenerateAndSave(CategoryRepository);

            Command = new ProductCreateCommand
            {
                Title = "Product Title",
                Description = "Product description",
                Price = 150,
                Quantity = 10,
                CategoryId = category.Id
            };
        }

        [Fact]
        public async Task Title_Must_Be_Not_Empty()
        {
            Command.Title = string.Empty;

            var response = await HttpClient.PostAsJsonAsync($"{BasePath}Products", Command);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Title_Must_Be_Not_Null()
        {
            Command.Title = null;

            var response = await HttpClient.PostAsJsonAsync($"{BasePath}Products", Command);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Title_Must_Contain_At_Least_3_Characters()
        {
            Command.Title = "Hi";

            var response = await HttpClient.PostAsJsonAsync($"{BasePath}Products", Command);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Title_Must_Have_Less_Then_120_Characters()
        {
            Command.Title = Faker.Lorem.Letter(121);



            var response = await HttpClient.PostAsJsonAsync($"{BasePath}Products", Command);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Description_Must_Be_Not_Empty()
        {
            Command.Description = string.Empty;

            var response = await HttpClient.PostAsJsonAsync($"{BasePath}Products", Command);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Description_Must_Be_Not_Null()
        {
            Command.Description = string.Empty;

            var response = await HttpClient.PostAsJsonAsync($"{BasePath}Products", Command);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Description_Must_Contain_At_Least_3_Characters()
        {
            Command.Description = "Hi";

            var response = await HttpClient.PostAsJsonAsync($"{BasePath}Products", Command);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Description_Must_Have_Less_Then_1024_Characters()
        {
            Command.Description = Faker.Lorem.Letter(1025);

            var response = await HttpClient.PostAsJsonAsync($"{BasePath}Products", Command);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Price_Must_Be_Greather_Or_Equals_To_Zero()
        {
            Command.Price = -1;

            var response = await HttpClient.PostAsJsonAsync($"{BasePath}Products", Command);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Quantity_Must_Be_Greather_Or_Equals_To_Zero()
        {
            Command.Quantity = -1;

            var response = await HttpClient.PostAsJsonAsync($"{BasePath}Products", Command);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Category_Must_Exists()
        {
            Command.CategoryId = 999;
            var response = await HttpClient.PostAsJsonAsync($"{BasePath}Products", Command);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Must_Save()
        {
            var response = await HttpClient.PostAsJsonAsync($"{BasePath}Products", Command);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Must_Update()
        {
            var product = ProductGenerator.GenerateAndSave(ProductRepository, Command.CategoryId);
            Command.Id = product.Id;

            var response = await HttpClient.PutAsJsonAsync($"{BasePath}Products", Command);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            product = ProductRepository.Reload(product);
            Assert.Equal(Command.Id, product.Id);
            Assert.Equal(Command.Title, product.Title);
        }

    }
}
