using FluentValidation;
using SBCW.BLL.DTOs.Requests;

namespace SBCW.BLL.Validators;

public class ProductValidator : AbstractValidator<ProductRequest>
{
    public ProductValidator()
    {
        RuleFor(product => product.Name).NotEmpty().MaximumLength(50);
        RuleFor(product => product.Description).MaximumLength(500);
        RuleFor(product => product.Image).MaximumLength(255).When(product => !string.IsNullOrEmpty(product.Image));
        RuleFor(product => product.Link).MaximumLength(255).When(product => !string.IsNullOrEmpty(product.Link));
        RuleFor(product => product.Type).NotEmpty().MaximumLength(50);
    }
}