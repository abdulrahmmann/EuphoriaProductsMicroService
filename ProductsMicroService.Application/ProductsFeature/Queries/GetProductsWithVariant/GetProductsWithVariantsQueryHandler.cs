using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.ProductsFeature.Queries.GetProductsWithVariant;

public class GetProductsWithVariantsQueryHandler
    : IQueryHandler<GetProductsWithVariantsQuery, BaseResponse<IEnumerable<ProductWithVariantsDto>>>
{
    #region Create and Inject Instances.
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public GetProductsWithVariantsQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    #endregion
    
    
    public async Task<BaseResponse<IEnumerable<ProductWithVariantsDto>>> Handle(GetProductsWithVariantsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var products = await (
                from p in _dbContext.Products.AsNoTracking()
                join c in _dbContext.Categories.AsNoTracking() on p.CategoryId equals c.Id
                join b in _dbContext.Brands.AsNoTracking() on p.BrandId equals b.Id
                join sc in _dbContext.SubCategories.AsNoTracking() on p.SubCategoryId equals sc.Id
                select new
                {
                    p.Id,
                    p.Name,
                    p.Price,
                    p.TotalStock,
                    BrandName = b.Name,
                    CategoryName = c.Name,
                    SubCategoryName = sc.Name
                }
            ).ToListAsync(cancellationToken);

            var variants = await (
                from v in _dbContext.ProductVariants.AsNoTracking()
                join col in _dbContext.Colors.AsNoTracking() on v.ColorId equals col.Id
                join s in _dbContext.Sizes.AsNoTracking() on v.SizeId equals s.Id
                where !v.IsDeleted && !col.IsDeleted && !s.IsDeleted
                select new ProductVariantDto(
                    v.Id,
                    col.Name,
                    s.Name,
                    v.Stock,
                    v.PriceOverride
                )
                {
                    ProductId = v.ProductId
                }
            ).ToListAsync(cancellationToken);

            // Combine (group variants per product)
            var result = products.Select(p =>
                new ProductWithVariantsDto(
                    p.Id,
                    p.Name,
                    p.Price,
                    p.TotalStock,
                    p.BrandName,
                    p.CategoryName,
                    p.SubCategoryName,
                    variants.Where(v => v.ProductId == p.Id).ToList()
                )
            );

            return BaseResponse<IEnumerable<ProductWithVariantsDto>>.Success(result);
        }
        catch (Exception e)
        {
            return BaseResponse<IEnumerable<ProductWithVariantsDto>>.InternalError($"Unexpected error: {e.Message}");
        }
    }
}