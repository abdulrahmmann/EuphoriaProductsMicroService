using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.ProductsFeature.Queries.GetProducts;

public record GetProductsQuery(): IQuery<BaseResponse<IEnumerable<ProductDto>>>;