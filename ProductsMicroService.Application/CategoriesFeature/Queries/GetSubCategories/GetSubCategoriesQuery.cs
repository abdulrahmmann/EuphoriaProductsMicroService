using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.CategoriesFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.CategoriesFeature.Queries.GetSubCategories;

public record GetSubCategoriesQuery(): IQuery<BaseResponse<IEnumerable<SubCategoryDto>>>;