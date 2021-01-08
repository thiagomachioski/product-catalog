using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Products.Catalog.Domain.Categories;
using Products.Catalog.Domain.Products;
using Products.Catalog.Repository.Categories;
using Products.Catalog.Repository.Data;
using Products.Catalog.Repository.Products;
using Products.Catalog.UI.Categories;
using Products.Catalog.UI.Categories.Dtos;
using Products.Catalog.UI.Products.Dtos;

namespace Products.Catalog.UI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<StoreDataContext, StoreDataContext>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            
            services.AddControllers()
                .AddFluentValidation(e => 
                    e.RegisterValidatorsFromAssemblyContaining<CategoryValidator>()
                );
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Products.Catalog.UI", Version = "v1"});
            });
            
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                /*Category DTOs */
                /*POST AND PUT*/
                cfg.CreateMap<CategoryCommand, Category>();
                cfg.CreateMap<Category, CategoryCreateResult>();

                /*GET*/
                cfg.CreateMap<Category, ListCategoryResult>();
                cfg.CreateMap<Product, ListProductForCategoryResult>();

                /*Products DTOs */
                /*POST AND PUT*/
                cfg.CreateMap<ProductCreateCommand, Product>();
                cfg.CreateMap<ProductUpdateCommand, Product>();
                cfg.CreateMap<Product, ProductCreateResult>();
                cfg.CreateMap<Product, ProductUpdateResult>();
                

                /*GET*/
                cfg.CreateMap<Product, FindProductByIdResult>(); 
                cfg.CreateMap<Product, ListProductResult>();
                cfg.CreateMap<Category, CategoryForProductResult>();
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products.Catalog.UI v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}