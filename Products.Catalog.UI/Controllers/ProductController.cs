using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.Catalog.Domain.Products;
using Products.Catalog.Repository.BlobStorage;
using Products.Catalog.UI.Extensions;
using Products.Catalog.UI.Products.Dtos;

namespace Products.Catalog.UI.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IBlobStorageProvider _blobStorageProvider;

        public ProductController(
            IProductRepository repository, 
            IMapper mapper,
            IBlobStorageProvider blobStorageProvider
        )
        {
            _repository = repository;
            _mapper = mapper;
            _blobStorageProvider = blobStorageProvider;
        }

        [Route("v1/products")]
        [HttpGet]
        public IEnumerable<ListProductResult> Get()
        {
            var products = _repository.Get();
            return _mapper.Map<List<ListProductResult>>(products);
        }

        [Route("v1/products/{id}")]
        [HttpGet]
        public FindProductByIdResult Get(int id)
        {
            var product = _repository.GetById(id);
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

            var product = _mapper.Map<Product>(command);
            _repository.Save(product);

            return new OkObjectResult(
                new ResultViewModel
                {
                    Success = true,
                    Message = "Produto cadastrado com sucesso",
                    Data = _mapper.Map<ProductCreateResult>(product)
                });
        }
        
        
        [HttpPut("v1/products/{id}/AddImage")]
        public string AddImage([FromRoute] int id, IFormFile file)
        {
            return _blobStorageProvider.Upload(file.OpenReadStream(), file.Name);
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

            var product = _repository.GetById(command.Id);
            if (product == null)
                return new NotFoundObjectResult($"Produto com o Id {command.Id} não encontrada!");
            
            product = _mapper.Map<Product>(command);
            //Salvar
            _repository.Update(product);

            return new OkObjectResult(
                new ResultViewModel
                {
                    Success = true,
                    Message = "Produto atualizado com sucesso",
                    Data = _mapper.Map<ProductUpdateResult>(product)
                });
        }
    }
}
