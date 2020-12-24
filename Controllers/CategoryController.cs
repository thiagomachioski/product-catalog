using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Products.Catalog.Models;
using Products.Catalog.Repositories;
using Products.Catalog.ViewModels;
using Products.Catalog.ViewModels.CategoryViewModels;

namespace Products.Catalog.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly CategoryRepository _repository;

        public CategoryController(CategoryRepository repository)
        {
            _repository = repository;
        }

        [Route("v1/categories")]
        [HttpGet]
        public IEnumerable<ListCategoryViewModel> Get()
        {
            return _repository.Get();
        }

        [Route("v1/categories/{id}")]
        [HttpGet]
        public ListCategoryViewModel Get(int id)
        {
            return _repository.GetById(id);
        }

        [Route("v1/categories")]
        [HttpPost]
        public IActionResult Post([FromBody] EditorCategoryViewModel model)
        {

            model.Validate();
            if (model.Invalid)
            {
                return new BadRequestObjectResult(
                  new ResultViewModel
                  {
                      Success = false,
                      Message = "Erro ao cadastrar categoria",
                      Data = model.Notifications
                  });
            }
            var category = new Category();
            category.Title = model.Title;

            _repository.Save(category);

            return new OkObjectResult(
                new ResultViewModel
                {
                    Success = true,
                    Message = "Categoria Cadastrada com sucesso",
                    Data = category
                });
        }

        [Route("v1/categories")]
        [HttpPut]
        public IActionResult Put([FromBody] EditorCategoryViewModel model)
        {
            model.Validate();
            if (model.Invalid)
            {
                return new BadRequestObjectResult(
                  new ResultViewModel
                  {
                      Success = false,
                      Message = "Erro ao cadastrar categoria",
                      Data = model.Notifications
                  });
            }

            var category = _repository.Get(model.Id);
            category.Title = model.Title;

            _repository.Update(category);

            return new OkObjectResult(
                new ResultViewModel
                {
                    Success = true,
                    Message = "Categoria Cadastrada com sucesso",
                    Data = category
                });
        }

    }
}
