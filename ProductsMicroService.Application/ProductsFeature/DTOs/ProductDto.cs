namespace ProductsMicroService.Application.ProductsFeature.DTOs;

public record ProductDto(
    int Id,
    string Name, 
    string Description,
    decimal Price,
    int TotalStock,
    List<string> ProductImages,
    
    int BrandId,
    string BrandName,
    
    int CategoryId,
    string CategoryName,
    
    int SubCategoryId,
    string SubCategoryName
);