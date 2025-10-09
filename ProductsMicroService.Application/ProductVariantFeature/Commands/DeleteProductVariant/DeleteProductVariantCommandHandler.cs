using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.ProductVariantFeature.Commands.DeleteProductVariant;

public class DeleteProductVariantCommandHandler: ICommandHandler<DeleteProductVariantCommand, BaseResponse<Unit>>
{
    #region Create and Inject Instances.
    private readonly ApplicationDbContext  _dbContext;
    private readonly IMapper _mapper;
    
    public DeleteProductVariantCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(DeleteProductVariantCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.VariantId <= 0) 
                return BaseResponse<Unit>.ValidationError("Product Variant Id must be greater than zero");
            
            var variant = await 
                _dbContext.ProductVariants.FirstOrDefaultAsync(p => p.Id == request.VariantId, cancellationToken);

            if (variant == null)
            {
                return BaseResponse<Unit>.NotFound($"Product Variant by id: {request.VariantId} not found");
            }
            
            variant.SoftDelete();

            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return BaseResponse<Unit>.Success($"Product Variant with id: {request.VariantId} deleted successfully");
        }
        catch (Exception e)
        {
            return BaseResponse<Unit>.InternalError($"Unexpected error: {e.Message}");
        }
    }
}