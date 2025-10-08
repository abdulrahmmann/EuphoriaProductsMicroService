using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.BrandFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.BrandFeature.Queries.GetBrands;

public class GetBrandsQueryHandler: IQueryHandler<GetBrandsQuery, BaseResponse<IEnumerable<BrandDto>>>
{
    #region Field Instance
    private readonly ApplicationDbContext  _dbContext;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public GetBrandsQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    #endregion
    
    public async Task<BaseResponse<IEnumerable<BrandDto>>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var brands = await _dbContext.Brands.ToListAsync(cancellationToken);

            var brandsMap = _mapper.Map<IEnumerable<BrandDto>>(brands);
            
            return BaseResponse<IEnumerable<BrandDto>>.Success(brandsMap, totalCount:brands.Count);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<IEnumerable<BrandDto>>.ValidationError(e.Message);
        }
    }
}