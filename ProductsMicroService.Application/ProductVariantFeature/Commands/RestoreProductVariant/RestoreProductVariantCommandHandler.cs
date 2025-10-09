using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.ProductVariantFeature.Commands.RestoreProductVariant;

public class RestoreProductVariantCommandHandler: ICommandHandler<RestoreProductVariantCommand, BaseResponse<Unit>>
{
    #region Create and Inject Instances
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public RestoreProductVariantCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    #endregion
    
    public async Task<BaseResponse<Unit>> Handle(RestoreProductVariantCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.VariantId <= 0)
                return BaseResponse<Unit>.ValidationError("Product Variant Id must be greater than zero.");

            var product = await _dbContext.ProductVariants
                .IgnoreQueryFilters() 
                .FirstOrDefaultAsync(p => p.Id == request.VariantId, cancellationToken);

            if (product == null)
                return BaseResponse<Unit>.NotFound($"Product Variant with id {request.VariantId} not found.");

            product.RestoreDeletedProductVariant();

            await _dbContext.SaveChangesAsync(cancellationToken);

            return BaseResponse<Unit>.Success($"Product Variant with id {request.VariantId} restored successfully.");
        }
        catch (Exception e)
        {
            return BaseResponse<Unit>.InternalError($"Unexpected error: {e.Message}");
        }
    }
}