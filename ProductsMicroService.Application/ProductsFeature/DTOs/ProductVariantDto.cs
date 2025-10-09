namespace ProductsMicroService.Application.ProductsFeature.DTOs;

public record ProductVariantDto(
    int Id,
    string ColorName,
    string SizeName,
    int Stock,
    decimal? PriceOverride
)
{
    public int ProductId { get; init; } 
}