using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.BrandFeature.DTOs;
using ProductsMicroService.Application.ColorsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.ColorsFeature.Queries.GetColors;

public record GetColorsQuery(): IQuery<BaseResponse<IEnumerable<ColorDto>>>;