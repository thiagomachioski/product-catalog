using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Products.Catalog.Domain.Categories;
using Products.Catalog.UI.Categories.Dtos;
using Products.Catalog.UI.Extensions;

namespace Products.Catalog.UI.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [Route("v1/categories")]
        [HttpGet]
        public IEnumerable<ListCategoryResult> Get()
        {
            var categories = _repository.Get();
            return _mapper.Map<List<ListCategoryResult>>(categories);
        }

        [Route("v1/categories/{id}")]
        [HttpGet]
        public ListCategoryResult Get(int id)
        {
            var category = _repository.GetById(id);
            return _mapper.Map<ListCategoryResult>(category);
        }

        [Route("v1/categories")]
        [HttpPost]
        public IActionResult Post([FromBody] CategoryCommand command)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(
                    new ResultViewModel
                    {
                        Success = false,
                        Message = "Erro ao cadastrar categoria",
                        Data = ModelState.GetErrors()
                    });

            var category = _mapper.Map<Category>(command);
            _repository.Save(category);

            return new OkObjectResult(
                new ResultViewModel
                {
                    Success = true,
                    Message = "Categoria Cadastrada com sucesso",
                    Data = _mapper.Map<CategoryCreateResult>(category)
                });
        }

        [Route("v1/categories")]
        [HttpPut]
        public IActionResult Put([FromBody] CategoryCommand command)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(
                    new ResultViewModel
                    {
                        Success = false,
                        Message = "Erro ao cadastrar categoria",
                        Data = ModelState.GetErrors()
                    });

            var category = _repository.GetById(command.Id);

            if (category == null)
                return new NotFoundObjectResult($"Categoria com o Id {command.Id} não encontrada!");

            category.Title = command.Title;
            _repository.Update(category);

            return new OkObjectResult(
                new ResultViewModel
                {
                    Success = true,
                    Message = "Categoria Cadastrada com sucesso",
                    Data = _mapper.Map<CategoryCreateResult>(category)
                });
        }
    }
}