using FluentValidation;
using Products.Catalog.UI.Messages;
using Products.Catalog.UI.Products.Dtos;

namespace Products.Catalog.UI.Products
{
    public class ProductUpdateValidator : AbstractValidator<ProductUpdateCommand>
    {
        public ProductUpdateValidator()
        {
            RuleFor(e => e.Description)
                .NotNull().WithMessage(ValidationMessages.DescriptionNotNullMessage)
                .NotEmpty().WithMessage(ValidationMessages.DescriptionNotEmptyMessage)
                .MinimumLength(3).WithMessage(ValidationMessages.DescriptionMinimumLengthMessage)
                .MaximumLength(1024).WithMessage(ValidationMessages.DescriptionMaxiumLengthMessage);

            RuleFor(e => e.Title)
                .NotNull().WithMessage(ValidationMessages.TitleNotEmptyMessage)
                .NotEmpty().WithMessage(ValidationMessages.TitleNotEmptyMessage)
                .MinimumLength(3).WithMessage(ValidationMessages.TitleMinimumLengthMessage)
                .MaximumLength(120).WithMessage(ValidationMessages.TitleMaxiumLengthMessage);

            RuleFor(e => e.Price)
                .NotNull().WithMessage(ValidationMessages.PriceNotNullMessage)
                .NotEmpty().WithMessage(ValidationMessages.PriceNotEmptyMessage)
                .GreaterThan(0).WithMessage(ValidationMessages.PriceGreatherThanMessage);

            RuleFor(e => e.Quantity)
                .NotNull().WithMessage(ValidationMessages.QuantityNotNullMessage)
                .NotEmpty().WithMessage(ValidationMessages.QuantityNotEmptyMessage)
                .GreaterThan(0).WithMessage(ValidationMessages.QuantityGreatherThanMessage);

            RuleFor(e => e.CategoryId)
                .NotNull().WithMessage(ValidationMessages.CategoryIdNotNullMessage)
                .NotEmpty().WithMessage(ValidationMessages.CategoryIdNotEmptyMessage)
                .GreaterThan(0).WithMessage(ValidationMessages.CategoryIdGreatherThanMessage);
        }
    }
}