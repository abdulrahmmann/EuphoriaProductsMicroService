using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.ProductsFeature.Commands.UpdateProduct;

public class UpdateProductCommandHandler: ICommandHandler<UpdateProductCommand, BaseResponse<Unit>>
{
    #region Create and Inject Instances.
    private readonly ApplicationDbContext  _dbContext;
    private readonly IValidator<UpdateProductDto>  _validator;
    private readonly IMapper _mapper;
    
    public UpdateProductCommandHandler(ApplicationDbContext dbContext, IMapper mapper, IValidator<UpdateProductDto>  validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }
    #endregion
    
    public async Task<BaseResponse<Unit>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.ProductId <= 0) return BaseResponse<Unit>.ValidationError("Product Id must be greater than zero");
            
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

            if (product == null)
            {
                return BaseResponse<Unit>.NotFound($"Product by id: {request.ProductId} not found");
            }
            
            var validationResult = await _validator.ValidateAsync(request.ProductDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return BaseResponse<Unit>.ValidationError(errors);
            }
            
            product.UpdateProduct(
                request.ProductDto.Name,
                request.ProductDto.Description,
                request.ProductDto.Price,
                request.ProductDto.TotalStock,
                request.ProductDto.CategoryId,
                request.ProductDto.BrandId
            );
            
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return BaseResponse<Unit>.Success("Product updated successfully");
        }
        catch (Exception e)
        {
            return BaseResponse<Unit>.InternalError($"Unexpected error: {e.Message}");
        }
    }
}