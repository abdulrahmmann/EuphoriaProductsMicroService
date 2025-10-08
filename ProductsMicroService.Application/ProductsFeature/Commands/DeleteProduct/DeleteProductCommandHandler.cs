using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.ProductsFeature.Commands.DeleteProduct;

public class DeleteProductCommandHandler: ICommandHandler<DeleteProductCommand, BaseResponse<Unit>>
{
    #region Create and Inject Instances.
    private readonly ApplicationDbContext  _dbContext;
    private readonly IMapper _mapper;
    
    public DeleteProductCommandHandler(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    #endregion

    public async Task<BaseResponse<Unit>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.ProductId <= 0) return BaseResponse<Unit>.ValidationError("Product Id must be greater than zero");
            
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            if (product == null)
            {
                return BaseResponse<Unit>.NotFound($"Product by id: {request.ProductId} not found");
            }
            
            product.SoftDeleteProduct();

            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return BaseResponse<Unit>.Success($"Product with id: {request.ProductId} deleted successfully");
        }
        catch (Exception e)
        {
            return BaseResponse<Unit>.InternalError($"Unexpected error: {e.Message}");
        }
    }
}