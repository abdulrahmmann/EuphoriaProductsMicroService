using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.ProductsFeature.Queries.GetProductsByPriceRange;

public class GetProductsByPriceRangeQueryHandler
    : IQueryHandler<GetProductsByPriceRangeQuery, BaseResponse<IEnumerable<ProductDto>>>
{
    #region Create and Inject Instances.
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public GetProductsByPriceRangeQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    #endregion
    
    public async Task<BaseResponse<IEnumerable<ProductDto>>> Handle(GetProductsByPriceRangeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.MinPrice < 0 || request.MaxPrice <= 0)
                return BaseResponse<IEnumerable<ProductDto>>.ValidationError("Price range must be greater than zero.");

            if (request.MinPrice > request.MaxPrice)
                return BaseResponse<IEnumerable<ProductDto>>.ValidationError("MinPrice cannot be greater than MaxPrice.");
            
            var query =
                from p in _dbContext.Products.AsNoTracking()
                join c in _dbContext.Categories on p.CategoryId equals c.Id
                join s in _dbContext.SubCategories on p.SubCategoryId equals s.Id
                join b in _dbContext.Brands on p.BrandId equals b.Id
                where p.Price >= request.MinPrice && p.Price <= request.MaxPrice
                orderby p.Price
                select new ProductDto(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.TotalStock,
                    p.ProductImages.ToList(),
                    p.BrandId,
                    b.Name,
                    p.CategoryId,
                    c.Name,
                    p.SubCategoryId,
                    s.Name
                );

            var products = await query.ToListAsync(cancellationToken);

            if (products.Count == 0)
                return BaseResponse<IEnumerable<ProductDto>>.NotFound(
                    $"No products found in the price range {request.MinPrice:C} - {request.MaxPrice:C}."
                );

            return BaseResponse<IEnumerable<ProductDto>>.Success(
                data: products,
                totalCount: products.Count,
                message: $"Found {products.Count} products between {request.MinPrice:C} and {request.MaxPrice:C}."
            );
        }
        catch (Exception e)
        {
            return BaseResponse<IEnumerable<ProductDto>>.InternalError($"Unexpected error: {e.Message}");
        }
    }
}