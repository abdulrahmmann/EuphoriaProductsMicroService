using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.ProductsFeature.Queries.GetProductById;

public class GetProductByIdQueryHandler: IQueryHandler<GetProductByIdQuery, BaseResponse<ProductDto>>
{
    #region Create and Inject Instances.
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    #endregion
    
    public async Task<BaseResponse<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = 
                from p in _dbContext.Products
                join c in _dbContext.Categories on p.CategoryId equals c.Id
                join s in _dbContext.SubCategories on p.SubCategoryId equals s.Id
                join b in _dbContext.Brands on p.BrandId equals b.Id
                where p.Id == request.ProductId    
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
            
            var product = await query.FirstOrDefaultAsync(cancellationToken);

            if (product is null)
                return BaseResponse<ProductDto>.NotFound($"Product with id {request.ProductId} not found.");

            return BaseResponse<ProductDto>.Success(product);
        }
        catch (Exception e)
        {
            return BaseResponse<ProductDto>.ValidationError(e.Message);
        }
    }
}