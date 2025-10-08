namespace ProductsMicroService.Application.ProductsFeature.DTOs;

public record CreateProductDto(
    string Name, 
    string Description,
    decimal Price,
    int TotalStock,
    List<string> ProductImages,
    
    int BrandId,
    int CategoryId,
    int SubCategoryId
);