namespace ProductsMicroService.Application.ProductVariantFeature.DTOs;

public record UpdateProductVariantDto(
    decimal? PriceOverride, 
    int Stock, 

    int ProductId, 

    int ColorId, 

    int SizeId
);