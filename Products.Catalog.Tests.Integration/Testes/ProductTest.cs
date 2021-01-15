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
        public async Task Title_Must_Contain_On_Minimum_Three_Characters()
        {
            Command.Title = "Hi";

            var response = await HttpClient.PostAsJsonAsync($"{BasePath}Products", Command);
            Output.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Title_Must_Contain_On_Maximum_Hundred_Twenty_Characters()
        {
            Command.Title = "Percebemos, cada vez mais, que o consenso sobre a necessidade de " +
                "qualificação não pode mais se dissociar dos conhecimentos estratégicos para atingir a excelência. " +
                "O que temos que ter sempre em mente";

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
            Command.Description = " Percebemos, cada vez mais, que o consenso sobre a necessidade de qualificação não pode mais se dissociar dos conhecimentos " +
                "estratégicos para atingir a excelência. " +
                "O que temos que ter sempre em mente é que o comprometimento entre as equipes cumpre um papel essencial na formulação de alternativas às " +
                "soluções ortodoxas. Neste sentido, " +
                "a hegemonia do ambiente político exige a precisão e a definição das formas de ação" +
                "Acima de tudo, é fundamental ressaltar que a estrutura atual da organização auxilia a preparação e a composição das posturas dos órgãos " +
                "dirigentes com relação às suas atribuições." +
                "Do mesmo modo, a crescente influência da mídia garante a contribuição de um grupo importante na determinação do fluxo de informações." +
                "Por outro lado, o desenvolvimento contínuo de distintas" +
                "formas de atuação é uma das consequências do levantamento das variáveis envolvidas." +
                "Todas estas questões, devidamente ponderadas, levantam dúvidas sobre se a contínua expansão de nossa atividade aponta para a " +
                "melhoria das direções preferenciais no sentido do progresso";

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
