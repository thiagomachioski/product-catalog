using FluentValidation;
using Products.Catalog.UI.Categories.Dtos;

namespace Products.Catalog.UI.Categories
{
    public class CategoryValidator: AbstractValidator<CategoryCommand>
    {
        public CategoryValidator()
        {
            RuleFor(e => e.Title)
                .NotEmpty().WithMessage("O titulo não pode ser nulo")
                .NotNull().WithMessage("O titulo não pode ser vazio")
                .MinimumLength(3).WithMessage("O titulo deve conter pelo menos 3 caracteres")
                .MaximumLength(120).WithMessage("O titulo deve conter até 120 caracteres");
        }
    }
}