using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductVariantFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.ProductVariantFeature.Queries.GetAllWithDetails;

public record GetProductVariantsQuery(): IQuery<BaseResponse<IEnumerable<ProductVariantDto>>>;