using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Products.Catalog.Domain.Categories;
using Xunit;
using Xunit.Abstractions;

namespace Products.Catalog.Tests.Integration.Testes.Common
{
    public class BaseTest : IClassFixture<TestWebApplicationFactory>
    {
        protected readonly string BasePath;
        protected readonly ITestOutputHelper Output;
        protected readonly HttpClient HttpClient;
        
        protected readonly ICategoryRepository CategoryRepository;

        protected BaseTest(TestWebApplicationFactory factory, ITestOutputHelper output)
        {
            BasePath = "v1/";
            Output = output;
            var scope = factory.Server.Services.CreateScope();
            HttpClient = factory.CreateClient();
            
            CategoryRepository = scope.ServiceProvider.GetRequiredService<ICategoryRepository>();
        }
    }
}