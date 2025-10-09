using FluentValidation;
using ProductsMicroService.Application.ProductVariantFeature.DTOs;

namespace ProductsMicroService.Application.ProductVariantFeature.Validators;

public class CreateProductVariantValidator: AbstractValidator<CreateProductVariantDto>
{
    public CreateProductVariantValidator()
    {
        RuleFor(v => v.PriceOverride)
            .GreaterThan(0).WithMessage("Price must be greater than 0")
            .PrecisionScale(18, 2, true).WithMessage("Price must not exceed 18 digits with 2 decimals.");
        
        RuleFor(v => v.Stock)
            .NotEmpty().WithMessage("Price is required")
            .NotNull().WithMessage("Price is required")
            .GreaterThan(-1).WithMessage("Price must be a positive number");

        RuleFor(v => v.ProductId)
            .NotEmpty().WithMessage("ProductId is required")
            .NotNull().WithMessage("ProductId is required");
        
        RuleFor(v => v.ColorId)
            .NotEmpty().WithMessage("ColorId is required")
            .NotNull().WithMessage("ColorId is required");
        
        RuleFor(v => v.SizeId)
            .NotEmpty().WithMessage("SizeId is required")
            .NotNull().WithMessage("SizeId is required");
    }
}