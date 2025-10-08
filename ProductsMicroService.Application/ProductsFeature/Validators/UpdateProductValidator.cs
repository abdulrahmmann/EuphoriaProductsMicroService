using FluentValidation;
using ProductsMicroService.Application.ProductsFeature.DTOs;

namespace ProductsMicroService.Application.ProductsFeature.Validators;

public class UpdateProductValidator: AbstractValidator<UpdateProductDto>
{
    public UpdateProductValidator()
    {
        RuleFor(p => p.Name)
            .MaximumLength(60).WithMessage("Name must not exceed 60 characters");
        
        RuleFor(p => p.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0")
            .PrecisionScale(18, 2, true).WithMessage("Price must not exceed 18 digits with 2 decimals.");

        RuleFor(p => p.TotalStock)
            .GreaterThan(-1).WithMessage("Price must be positive number");
    }
}