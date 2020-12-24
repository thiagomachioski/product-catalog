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

        [Route("v1/categories")]
        [HttpPost]
        public ResultViewModel Post([FromBody] EditorCategoryViewModel model)
        {
            var category = new Category();
            category.Title = model.Title;

            _repository.Save(category);

            return new ResultViewModel
            {
                Success = true,
                Message = "Categoria Cadastrada com sucesso",
                Data = category
            };
        }
    }
}
