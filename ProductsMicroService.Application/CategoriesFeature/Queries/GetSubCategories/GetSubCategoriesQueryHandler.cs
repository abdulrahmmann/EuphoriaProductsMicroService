using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.CategoriesFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.CategoriesFeature.Queries.GetSubCategories;

public class GetSubCategoriesQueryHandler: IQueryHandler<GetSubCategoriesQuery, BaseResponse<IEnumerable<SubCategoryDto>>>
{
    #region Field Instance
    private readonly ApplicationDbContext  _dbContext;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public GetSubCategoriesQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    #endregion
    
    public async Task<BaseResponse<IEnumerable<SubCategoryDto>>> Handle(GetSubCategoriesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var subCategories = await (
                from sub in _dbContext.SubCategories
                join cat in _dbContext.Categories on sub.CategoryId equals cat.Id
                select new SubCategoryDto(
                    sub.Id,
                    sub.Name,
                    sub.Description,
                    cat.Name,      
                    sub.CategoryId  
                )
            ).ToListAsync(cancellationToken);

            return BaseResponse<IEnumerable<SubCategoryDto>>.Success(subCategories, totalCount: subCategories.Count);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<IEnumerable<SubCategoryDto>>.ValidationError(e.Message);
        }
    }
}