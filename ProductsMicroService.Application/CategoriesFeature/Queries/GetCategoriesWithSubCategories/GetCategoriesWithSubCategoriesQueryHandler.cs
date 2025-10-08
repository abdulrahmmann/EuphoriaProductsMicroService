using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.CategoriesFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.CategoriesFeature.Queries.GetCategoriesWithSubCategories;

public class GetCategoriesWithSubCategoriesQueryHandler
    : IQueryHandler<GetCategoriesWithSubCategoriesQuery, BaseResponse<IEnumerable<CategoryWithSubCategoriesDto>>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetCategoriesWithSubCategoriesQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<BaseResponse<IEnumerable<CategoryWithSubCategoriesDto>>> Handle(GetCategoriesWithSubCategoriesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var flatData = await (
                from cat in _dbContext.Categories
                join sub in _dbContext.SubCategories on cat.Id equals sub.CategoryId into subGroup
                select new CategoryWithSubCategoriesDto(
                    cat.Id,
                    cat.Name,
                    cat.Description,
                    subGroup.Select(s => new SubCategoryDto(
                        s.Id,
                        s.Name,
                        s.Description,
                        cat.Name,
                        s.CategoryId
                    ))
                )
            ).ToListAsync(cancellationToken);
            
            return BaseResponse<IEnumerable<CategoryWithSubCategoriesDto>>.Success(flatData, totalCount: flatData.Count);
        }
        catch (Exception e)
        {
            return BaseResponse<IEnumerable<CategoryWithSubCategoriesDto>>.InternalError($"Unexpected error: {e.Message}");
        }
    }
}