using MediatR;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.BrandFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.BrandFeature.Commands.CreateBrand;

public record CreateBrandCommand(CreateBrandDto BrandDto): IQuery<BaseResponse<Unit>>;