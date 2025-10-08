using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.ProductsFeature.Queries.SearchProductByName;

public record GetProductByNameQuery(string ProductName): IQuery<BaseResponse<IEnumerable<ProductDto>>>;