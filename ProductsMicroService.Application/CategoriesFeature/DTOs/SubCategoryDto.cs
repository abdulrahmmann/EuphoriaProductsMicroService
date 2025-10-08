namespace ProductsMicroService.Application.CategoriesFeature.DTOs;

public record SubCategoryDto(int Id, string Name, string? Description, string CategoryName, int CategoryId);