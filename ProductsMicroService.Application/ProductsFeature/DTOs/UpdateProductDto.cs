namespace ProductsMicroService.Application.ProductsFeature.DTOs;

public record UpdateProductDto(
    string? Name, 
    string? Description,
    decimal? Price,
    int? TotalStock,
    List<string?> ProductImages,
    
    int? BrandId,
    int? CategoryId,
    int? SubCategoryId
);