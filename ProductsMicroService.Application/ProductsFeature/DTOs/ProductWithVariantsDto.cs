namespace ProductsMicroService.Application.ProductsFeature.DTOs;

public record ProductWithVariantsDto(
    int Id,
    string Name,
    decimal Price,
    int TotalStock,
    string BrandName,
    string CategoryName,
    string SubCategoryName,
    List<ProductVariantDto> Variants
);