using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.ProductsFeature.Queries.GetProductById;

public record GetProductByIdQuery(int ProductId): IQuery<BaseResponse<ProductDto>>;