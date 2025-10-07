using MediatR;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.BrandFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.BrandFeature.Commands.CreateBrandList;

public record CreateBrandListCommand(List<CreateBrandDto> Brands) : ICommand<BaseResponse<Unit>>;
