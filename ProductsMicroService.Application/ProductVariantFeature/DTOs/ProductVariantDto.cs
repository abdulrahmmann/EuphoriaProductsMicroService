namespace ProductsMicroService.Application.ProductVariantFeature.DTOs;

public record ProductVariantDto(
    int Id, 
    decimal? PriceOverride, 
    int Stock, 

    int ProductId, 
    string ProductName, 

    int ColorId, 
    string ColorName, 

    int SizeId, 
    string SizeName
);