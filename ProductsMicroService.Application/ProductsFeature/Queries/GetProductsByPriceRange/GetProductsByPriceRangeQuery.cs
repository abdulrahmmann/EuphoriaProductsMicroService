using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.ProductsFeature.Queries.GetProductsByPriceRange;

public record GetProductsByPriceRangeQuery(decimal MinPrice, decimal MaxPrice)
    : IQuery<BaseResponse<IEnumerable<ProductDto>>>;