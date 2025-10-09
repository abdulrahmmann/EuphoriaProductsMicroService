namespace ProductsMicroService.Application.ProductVariantFeature.DTOs;

public record ProductVariantDto(
    int Id, 
    decimal? PriceOverride, 
    int Stock, 

    int ProductId, 
    string ProductName, 
    string ProductDescription, 
    int TotalStock, 
    int CategoryId, 
    int SubCategoryId, 
    int BrandId, 

    int ColorId, 
    string ColorName, 

    int SizeId, 
    string SizeName
);