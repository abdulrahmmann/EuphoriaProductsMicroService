using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.CategoriesFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.CategoriesFeature.Queries.GetCategories;

public record GetCategoriesQuery(): IQuery<BaseResponse<IEnumerable<CategoryDto>>>;