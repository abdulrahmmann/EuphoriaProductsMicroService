using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.BrandFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.BrandFeature.Queries.GetBrandById;

public record GetBrandByIdQuery(int BrandId): IQuery<BaseResponse<BrandDto>>;