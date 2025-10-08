using FluentValidation;
using ProductsMicroService.Application.CategoriesFeature.DTOs;

namespace ProductsMicroService.Application.CategoriesFeature.Validators;

public class CreateCategoryValidator: AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryValidator()
    {
        RuleFor(temp => temp.Name)
            .NotEmpty().WithMessage("Name is required")
            .NotNull().WithMessage("Name is required")
            .MaximumLength(30).WithMessage("Name must not exceed 30 characters");
        
        RuleFor(temp => temp.Description)
            .MaximumLength(400).WithMessage("Description must not exceed 400 characters");
    }
}