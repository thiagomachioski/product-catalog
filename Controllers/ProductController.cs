using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Products.Catalog.Models;
using Products.Catalog.Repositories;
using Products.Catalog.ViewModels;
using Products.Catalog.ViewModels.ProductViewModels;

namespace Products.Catalog.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ProductRepository _repository;

        public ProductController(ProductRepository repository)
        {
            _repository = repository;
        }

        [Route("v1/products")]
        [HttpGet]
        public IEnumerable<ListProductViewModel> Get()
        {
            return _repository.Get();
        }

        [Route("v1/products/{id}")]
        [HttpGet]
        public FindProductByIdViewModel Get(int id)
        {
            return _repository.GetById(id);
        }
        
        [Route("v1/products")]
        [HttpPost]
        public IActionResult Post([FromBody] EditorProductViewModel model)
        {

            model.Validate();
            if (model.Invalid)
                return new BadRequestObjectResult(
                    new ResultViewModel
                    {
                        Success = false,
                        Message = "Erro ao cadastrar o produto",
                        Data = model.Notifications
                    });

            var product = new Product();
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            product.CreateDate = DateTime.Now;
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = model.Price;
            product.Quantity = model.Quantity;

            _repository.Save(product);

            return new OkObjectResult(
                new ResultViewModel
                {
                    Success = true,
                    Message = "Produto cadastrado com sucesso",
                    Data = product
                });
        }

        [Route("v1/products")]
        [HttpPut]
        public IActionResult Put([FromBody] EditorProductViewModel model)
        {
            model.Validate();
            if (model.Invalid)
                return new BadRequestObjectResult(
                    new ResultViewModel
                    {
                        Success = false,
                        Message = "Erro ao cadastrar o produto",
                        Data = model.Notifications
                    });
            var product = _repository.Get(model.Id);
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            product.CreateDate = DateTime.Now;
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = model.Price;
            product.Quantity = model.Quantity;

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
