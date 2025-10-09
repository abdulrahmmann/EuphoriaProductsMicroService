using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Application.ProductVariantFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Infrastructure.Context;
using ProductVariantDto = ProductsMicroService.Application.ProductVariantFeature.DTOs.ProductVariantDto;

namespace ProductsMicroService.Application.ProductVariantFeature.Queries.GetAllWithDetails;

public class GetProductVariantsQueryHandler: 
    IQueryHandler<GetProductVariantsQuery, BaseResponse<IEnumerable<ProductVariantDto>>>
{
    #region Create and Inject Instances.
    private readonly ApplicationDbContext  _dbContext;
    private readonly IMapper _mapper;
    
    public GetProductVariantsQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    #endregion
    
    
    public async Task<BaseResponse<IEnumerable<ProductVariantDto>>> Handle(GetProductVariantsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var variants = await _dbContext.Products.ToListAsync(cancellationToken);

            if (variants.Count == 0)
            {
                return BaseResponse<IEnumerable<ProductVariantDto>>.NoContent();
            }
            
            var query =
                from v in _dbContext.ProductVariants
                join p in _dbContext.Products on v.ProductId equals p.Id
                join c in _dbContext.Colors on v.ColorId equals c.Id
                join s in _dbContext.Sizes on v.SizeId equals s.Id
                select new ProductVariantDto (
                    v.Id,
                    v.PriceOverride,
                    v.Stock,
                    v.ProductId,
                    
                    p.Name,
                    p.Description,
                    p.TotalStock,
                    p.CategoryId,
                    p.SubCategoryId,
                    p.BrandId,
                    
                    v.ColorId,
                    c.Name,
                    
                    v.SizeId,
                    s.Name
                );
            
            return BaseResponse<IEnumerable<ProductVariantDto>>.Success(query, totalCount: variants.Count);
        }
        catch (Exception e)
        {
            return BaseResponse<IEnumerable<ProductVariantDto>>.InternalError($"Unexpected error: {e.Message}");
        }
    }
}