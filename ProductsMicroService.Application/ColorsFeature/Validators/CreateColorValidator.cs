using FluentValidation;
using ProductsMicroService.Application.ColorsFeature.DTOs;

namespace ProductsMicroService.Application.ColorsFeature.Validators;

public class CreateColorValidator: AbstractValidator<CreateColorDto>
{
    public CreateColorValidator()
    {
        RuleFor(clr => clr.Name)
            .NotEmpty().WithMessage("Name is required")
            .NotNull().WithMessage("Name is required")
            .MaximumLength(60).WithMessage("Name must not exceed 60 characters");
        
        RuleFor(clr => clr.HexCode)
            .NotEmpty().WithMessage("Name is required")
            .NotNull().WithMessage("Name is required")
            .MaximumLength(60).WithMessage("HexCode must not exceed 60 characters");
    }
}