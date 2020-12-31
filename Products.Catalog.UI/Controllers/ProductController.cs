using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Products.Catalog.Domain.Products;
using Products.Catalog.UI.Extensions;
using Products.Catalog.UI.Products.Dtos;

namespace Products.Catalog.UI.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [Route("v1/products")]
        [HttpGet]
        public IEnumerable<ListProductResult> Get()
        {
            var products = _repository.Get();
            return products.Select(x => new ListProductResult
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Category = new CategoryForProductResult {Id = x.CategoryId, Title = x.Category.Title}
            });
        }

        [Route("v1/products/{id}")]
        [HttpGet]
        public FindProductByIdResult Get(int id)
        {
            var product = _repository.GetById(id);
            return new FindProductByIdResult
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                Image = product.Image,
                Category = new CategoryForProductResult
                {
                    Id = product.CategoryId, 
                    Title = product.Category.Title
                }
            };
        }
        
        [Route("v1/products")]
        [HttpPost]
        public IActionResult Post([FromBody] ProductCommand command)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(
                    new ResultViewModel
                    {
                        Success = false,
                        Message = "Erro ao cadastrar o produto",
                        Data = ModelState.GetErrors()
                    });

            var product = new Product();
            product.Title = command.Title;
            product.CategoryId = command.CategoryId;
            product.CreateDate = DateTime.Now;
            product.Description = command.Description;
            product.Image = command.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = command.Price;
            product.Quantity = command.Quantity;

            _repository.Save(product);
            product = _repository.GetById(product.Id);

            var viewModel = new FindProductByIdResult
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                Image = product.Image,
                Category = new CategoryForProductResult
                {
                    Id = product.CategoryId,
                    Title = product.Category.Title
                }
            };

            return new OkObjectResult(
                new ResultViewModel
                {
                    Success = true,
                    Message = "Produto cadastrado com sucesso",
                    Data = viewModel
                });
        }

        [Route("v1/products")]
        [HttpPut]
        public IActionResult Put([FromBody] ProductCommand command)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(
                    new ResultViewModel
                    {
                        Success = false,
                        Message = "Erro ao cadastrar o produto",
                        Data = ModelState.GetErrors()
                    });
            var product = _repository.GetById(command.Id);
            product.Title = command.Title;
            product.CategoryId = command.CategoryId;
            product.CreateDate = DateTime.Now;
            product.Description = command.Description;
            product.Image = command.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = command.Price;
            product.Quantity = command.Quantity;

            //Salvar
            _repository.Update(product);

            return new OkObjectResult(
                new ResultViewModel
                {
                    Success = true,
                    Message = "Produto cadastrado com sucesso",
                    Data = product
                });
        }
    }
}
