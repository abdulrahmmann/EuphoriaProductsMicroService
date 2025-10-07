using FluentValidation;
using ProductsMicroService.Application.BrandFeature.DTOs;

namespace ProductsMicroService.Application.BrandFeature.Validators;

public class UpdateBrandValidator: AbstractValidator<UpdateBrandDto>
{
    public UpdateBrandValidator()
    {
        RuleFor(brand => brand.Name)
            .MaximumLength(60).WithMessage("Name must not exceed 60 characters");
    }
}