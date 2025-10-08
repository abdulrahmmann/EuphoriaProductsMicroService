namespace ProductsMicroService.Application.CategoriesFeature.DTOs;

public record CategoryWithSubCategoriesDto(
    int Id,
    string Name,
    string? Description,
    IEnumerable<SubCategoryDto> SubCategories
);
