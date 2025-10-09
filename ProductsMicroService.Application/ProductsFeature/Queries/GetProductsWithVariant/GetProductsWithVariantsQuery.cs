using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.ProductsFeature.Queries.GetProductsWithVariant;

public record GetProductsWithVariantsQuery() : IQuery<BaseResponse<IEnumerable<ProductWithVariantsDto>>>;