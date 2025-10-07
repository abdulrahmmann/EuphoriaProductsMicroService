using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.BrandFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.BrandFeature.Queries.GetBrands;

public record GetBrandsQuery(): IQuery<BaseResponse<IEnumerable<BrandDto>>>;