namespace ProductsMicroService.Application.CategoriesFeature.DTOs;

public record CreateSubCategoryDto(string Name, string? Description, int CategoryId);