using MediatR;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ColorsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.ColorsFeature.Commands.CreateColorList;

public record CreateColorListCommand(IEnumerable<CreateColorDto> Colors): ICommand<BaseResponse<Unit>>;