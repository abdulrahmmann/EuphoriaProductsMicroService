using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ColorsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.ColorsFeature.Queries.GetColors;

public class GetColorsQueryHandler: IQueryHandler<GetColorsQuery, BaseResponse<IEnumerable<ColorDto>>>
{
    #region Field Instance
    private readonly ApplicationDbContext  _dbContext;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public GetColorsQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    #endregion

    public async Task<BaseResponse<IEnumerable<ColorDto>>> Handle(GetColorsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var colors = await _dbContext.Colors.ToListAsync(cancellationToken);

            if (colors.Count == 0)
            {
                return BaseResponse<IEnumerable<ColorDto>>.NoContent();
            }
            
            var colorsMap = _mapper.Map<IEnumerable<ColorDto>>(colors);

            return BaseResponse<IEnumerable<ColorDto>>.Success(colorsMap, totalCount: colors.Count);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<IEnumerable<ColorDto>>.ValidationError(e.Message);
        }
    }
}