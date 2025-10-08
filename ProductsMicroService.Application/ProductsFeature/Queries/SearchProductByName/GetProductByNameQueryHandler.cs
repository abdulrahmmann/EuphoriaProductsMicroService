using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.ProductsFeature.Queries.SearchProductByName;

public class GetProductByNameQueryHandler: IQueryHandler<GetProductByNameQuery, BaseResponse<IEnumerable<ProductDto>>>
{
    #region Create and Inject Instances.
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetProductByNameQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    #endregion
    
    public async Task<BaseResponse<IEnumerable<ProductDto>>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.ProductName))
                return BaseResponse<IEnumerable<ProductDto>>.ValidationError("Product name must not be empty.");

            var searchTerm = request.ProductName.Trim().ToLower();

            // 1️⃣ Materialize simple projection first
            var productsData = await (
                from p in _dbContext.Products.AsNoTracking()
                join c in _dbContext.Categories on p.CategoryId equals c.Id
                join s in _dbContext.SubCategories on p.SubCategoryId equals s.Id
                join b in _dbContext.Brands on p.BrandId equals b.Id
                where EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%")
                orderby p.Name
                select new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.TotalStock,
                    p.ProductImages,
                    BrandId = p.BrandId,
                    BrandName = b.Name,
                    CategoryId = p.CategoryId,
                    CategoryName = c.Name,
                    SubCategoryId = p.SubCategoryId,
                    SubCategoryName = s.Name
                }
            ).ToListAsync(cancellationToken);

            // 2️⃣ Project in-memory to your DTO
            var result = productsData.Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Description,
                p.Price,
                p.TotalStock,
                p.ProductImages?.ToList() ?? new List<string>(),
                p.BrandId,
                p.BrandName,
                p.CategoryId,
                p.CategoryName,
                p.SubCategoryId,
                p.SubCategoryName
            )).ToList();

            if (!result.Any())
                return BaseResponse<IEnumerable<ProductDto>>.NotFound($"No products found matching '{request.ProductName}'.");

            return BaseResponse<IEnumerable<ProductDto>>.Success(
                data: result,
                totalCount: result.Count,
                message: $"Found {result.Count} product(s) matching '{request.ProductName}'."
            );
        }
        catch (Exception e)
        {
            return BaseResponse<IEnumerable<ProductDto>>.ValidationError(e.Message);
        }
    }
}