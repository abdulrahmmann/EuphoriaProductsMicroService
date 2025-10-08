using FluentValidation;
using ProductsMicroService.Application.ProductsFeature.DTOs;

namespace ProductsMicroService.Application.ProductsFeature.Validators;

public class CreateProductValidator: AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required")
            .NotNull().WithMessage("Name is required")
            .MaximumLength(60).WithMessage("Name must not exceed 60 characters");
        
        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Description is required")
            .NotNull().WithMessage("Description is required")
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

        RuleFor(p => p.Price)
            .NotEmpty().WithMessage("Price is required")
            .NotNull().WithMessage("Price is required")
            .GreaterThan(0).WithMessage("Price must be greater than 0")
            .PrecisionScale(18, 2, true).WithMessage("Price must not exceed 18 digits with 2 decimals.");

        RuleFor(p => p.TotalStock)
            .NotEmpty().WithMessage("Price is required")
            .NotNull().WithMessage("Price is required")
            .GreaterThan(-1).WithMessage("Price must be positive number");

        RuleFor(p => p.BrandId)
            .NotEmpty().WithMessage("BrandId is required")
            .NotNull().WithMessage("BrandId is required");
        
        RuleFor(p => p.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required")
            .NotNull().WithMessage("CategoryId is required");
        
        RuleFor(p => p.SubCategoryId)
            .NotEmpty().WithMessage("SubCategoryId is required")
            .NotNull().WithMessage("SubCategoryId is required");
    }
}