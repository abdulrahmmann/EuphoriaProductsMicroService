using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.BrandFeature.DTOs;
using ProductsMicroService.Application.CategoriesFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.CategoriesFeature.Queries.GetCategories;

public class GetCategoriesQueryHandler: IQueryHandler<GetCategoriesQuery, BaseResponse<IEnumerable<CategoryDto>>>
{
    #region Field Instance
    private readonly ApplicationDbContext  _dbContext;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public GetCategoriesQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    #endregion
    
    
    public async Task<BaseResponse<IEnumerable<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var categories = await _dbContext.Categories.ToListAsync(cancellationToken);

            var categoriesMap = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            
            return BaseResponse<IEnumerable<CategoryDto>>.Success(categoriesMap, totalCount:categories.Count);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<IEnumerable<CategoryDto>>.ValidationError(e.Message);
        }
    }
}