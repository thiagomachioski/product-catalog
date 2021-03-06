﻿using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.Catalog.Domain.Categories;
using Products.Catalog.Domain.Products;
using Products.Catalog.Repository.BlobStorage;
using Products.Catalog.UI.Extensions;
using Products.Catalog.UI.Products.Dtos;

namespace Products.Catalog.UI.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IBlobStorageProvider _blobStorageProvider;

        public ProductController(
            IProductRepository productRepository, 
            ICategoryRepository categoryRepository,
            IMapper mapper,
            IBlobStorageProvider blobStorageProvider
        )
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _blobStorageProvider = blobStorageProvider;
        }

        [Route("v1/products")]
        [HttpGet]
        public IEnumerable<ListProductResult> Get([FromQuery] int page = 1, [FromQuery] int itemsPerPage = 10, [FromQuery] string query = "")
        {
            var products = _productRepository.Get(page, itemsPerPage, query);
            return _mapper.Map<List<ListProductResult>>(products);
        }

        [Route("v1/products/{id}")]
        [HttpGet]
        public FindProductByIdResult Get(int id)
        {
            var product = _productRepository.GetById(id);
            return _mapper.Map<FindProductByIdResult>(product);
        }

        [Route("v1/products")]
        [HttpPost]
        public IActionResult Post([FromBody] ProductCreateCommand command)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(
                    new ResultViewModel
                    {
                        Success = false,
                        Message = "Erro ao cadastrar o produto",
                        Data = ModelState.GetErrors()
                    });

            var category = _categoryRepository.GetById(command.CategoryId);
            if (category == null)
                return new NotFoundObjectResult($"Categoria com o id {command.CategoryId} não encontrado!");

            var product = _mapper.Map<Product>(command);
            _productRepository.Save(product);

            return new OkObjectResult(
                new ResultViewModel
                {
                    Success = true,
                    Message = "Produto cadastrado com sucesso",
                    Data = _mapper.Map<ProductCreateResult>(product)
                });
        }
        
        [HttpPut("v1/products/{id}/AddImage")]
        public IActionResult AddImage([FromRoute] int id, IFormFile file)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
                return new NotFoundObjectResult($"Produto com o id {id} não encontrado");

            product.Image = _blobStorageProvider.Upload(file.OpenReadStream(), file.FileName); 
            _productRepository.Update(product);

            return new OkObjectResult(product.Image);
        }

        [Route("v1/products")]
        [HttpPut]
        public IActionResult Put([FromBody] ProductCreateCommand command)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(
                    new ResultViewModel
                    {
                        Success = false,
                        Message = "Erro ao cadastrar o produto",
                        Data = ModelState.GetErrors()
                    });

            var product = _productRepository.GetById(command.Id);
            if (product == null)
                return new NotFoundObjectResult($"Produto com o Id {command.Id} não encontrada!");
            
            product = _mapper.Map<Product>(command);
            //Salvar
            _productRepository.Update(product);

            return new OkObjectResult(
                new ResultViewModel
                {
                    Success = true,
                    Message = "Produto atualizado com sucesso",
                    Data = _mapper.Map<ProductCreateResult>(product)
                });
        }
    }
}
