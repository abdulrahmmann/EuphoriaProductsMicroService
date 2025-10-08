using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.ProductsFeature.Queries.GetProducts;

public class GetProductsQueryHandler: IQueryHandler<GetProductsQuery, BaseResponse<IEnumerable<ProductDto>>>
{
    #region Create and Inject Instances.
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    #endregion

    public async Task<BaseResponse<IEnumerable<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var products = await _dbContext.Products.ToListAsync(cancellationToken);

            if (products.Count == 0)
            {
                return BaseResponse<IEnumerable<ProductDto>>.NoContent();
            }

            var query = 
                from p in _dbContext.Products
                join c in _dbContext.Categories on p.CategoryId equals c.Id
                join s in _dbContext.SubCategories on p.CategoryId equals s.Id
                join b in _dbContext.Brands on p.BrandId equals b.Id
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
            
            return BaseResponse<IEnumerable<ProductDto>>.Success(query, totalCount: products.Count);
        }
        catch (Exception e)
        {
            return BaseResponse<IEnumerable<ProductDto>>.ValidationError(e.Message);
        }
    }
}