using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.CategoriesFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.CategoriesFeature.Queries.GetCategoriesWithSubCategories;

public record GetCategoriesWithSubCategoriesQuery()
    : IQuery<BaseResponse<IEnumerable<CategoryWithSubCategoriesDto>>>;
