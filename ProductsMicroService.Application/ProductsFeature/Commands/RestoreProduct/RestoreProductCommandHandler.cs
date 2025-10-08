using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.ProductsFeature.Commands.RestoreProduct;

public class RestoreProductCommandHandler : ICommandHandler<RestoreProductCommand, BaseResponse<Unit>>
{
    #region Create and Inject Instances
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public RestoreProductCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    #endregion

    public async Task<BaseResponse<Unit>> Handle(RestoreProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.ProductId <= 0)
                return BaseResponse<Unit>.ValidationError("Product Id must be greater than zero.");

            var product = await _dbContext.Products
                .IgnoreQueryFilters() 
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            if (product == null)
                return BaseResponse<Unit>.NotFound($"Product with id {request.ProductId} not found.");

            product.RestoreDeletedProduct();

            await _dbContext.SaveChangesAsync(cancellationToken);

            return BaseResponse<Unit>.Success($"Product with id {request.ProductId} restored successfully.");
        }
        catch (Exception e)
        {
            return BaseResponse<Unit>.InternalError($"Unexpected error: {e.Message}");
        }
    }
}