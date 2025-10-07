using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.BrandFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.BrandFeature.Queries.GetBrandById;

public class GetBrandByIdQueryHandler: IQueryHandler<GetBrandByIdQuery, BaseResponse<BrandDto>>
{
    #region Field Instance
    private readonly ApplicationDbContext  _dbContext;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public GetBrandByIdQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    #endregion


    public async Task<BaseResponse<BrandDto>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.BrandId <= 0) return BaseResponse<BrandDto>.ValidationError();

            var brand = await _dbContext.Brands.FirstOrDefaultAsync(b => b.Id == request.BrandId, cancellationToken);
            
            var brandMap = _mapper.Map<BrandDto>(brand);
            
            return BaseResponse<BrandDto>.Success(brandMap);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<BrandDto>.ValidationError(e.Message);
        }
    }
}