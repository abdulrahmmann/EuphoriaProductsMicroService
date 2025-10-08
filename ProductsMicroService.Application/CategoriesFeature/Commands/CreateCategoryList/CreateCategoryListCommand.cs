using MediatR;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.CategoriesFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.CategoriesFeature.Commands.CreateCategoryList;

public record CreateCategoryListCommand(IEnumerable<CreateCategoryDto> Categories): ICommand<BaseResponse<Unit>>;