namespace ProductsMicroService.Application.ProductVariantFeature.DTOs;

public record UpdateProductVariantDto(
    int Id, 
    decimal? PriceOverride, 
    int Stock, 

    int ProductId, 

    int ColorId, 

    int SizeId
);