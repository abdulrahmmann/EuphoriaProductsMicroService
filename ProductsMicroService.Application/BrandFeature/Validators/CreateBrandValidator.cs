using FluentValidation;
using ProductsMicroService.Application.BrandFeature.DTOs;

namespace ProductsMicroService.Application.BrandFeature.Validators;

public class CreateBrandValidator: AbstractValidator<CreateBrandDto>
{
    public CreateBrandValidator()
    {
        RuleFor(brand => brand.Name)
            .NotEmpty().WithMessage("Name is required")
            .NotNull().WithMessage("Name is required")
            .MaximumLength(60).WithMessage("Name must not exceed 60 characters");
    }
}