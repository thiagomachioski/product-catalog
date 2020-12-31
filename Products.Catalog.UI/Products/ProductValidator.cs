using FluentValidation;
using Products.Catalog.UI.Products.Dtos;

namespace Products.Catalog.UI.Products
{
    public class ProductValidator: AbstractValidator<ProductCommand>
    {
        public ProductValidator()
        {
            RuleFor(e => e.Title)
                .NotNull().WithMessage("O titulo não pode ser nulo")
                .NotEmpty().WithMessage("O titulo não pode ser vazio")
                .MinimumLength(3).WithMessage("O titulo deve conter pelo menos 3 caracteres")
                .MaximumLength(120).WithMessage("O titulo deve conter até 120 caracteres");

            RuleFor(e => e.Price)
                .NotNull().WithMessage("O preço não deve ser nulo")
                .GreaterThan(0).WithMessage("O preço deve ser maior que zero");
        }
    }
}