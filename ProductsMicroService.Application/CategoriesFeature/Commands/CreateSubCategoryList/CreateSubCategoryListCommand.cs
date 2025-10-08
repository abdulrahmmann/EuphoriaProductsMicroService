using MediatR;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.CategoriesFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.CategoriesFeature.Commands.CreateSubCategoryList;

public record CreateSubCategoryListCommand(IEnumerable<CreateSubCategoryDto> SubCategories): ICommand<BaseResponse<Unit>>;