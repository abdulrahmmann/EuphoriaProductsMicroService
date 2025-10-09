using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductVariantFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.ProductVariantFeature.Commands.UpdateProductVariant;

public class UpdateProductVariantCommandHandler: ICommandHandler<UpdateProductVariantCommand, BaseResponse<Unit>>
{
    #region Create and Inject Instances.
    private readonly ApplicationDbContext  _dbContext;
    private readonly IValidator<UpdateProductVariantDto>  _validator;
    private readonly IMapper _mapper;
    
    public UpdateProductVariantCommandHandler(ApplicationDbContext dbContext, IMapper mapper, IValidator<UpdateProductVariantDto>  validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }
    #endregion
    
    public async Task<BaseResponse<Unit>> Handle(UpdateProductVariantCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.VariantId <= 0) return BaseResponse<Unit>.ValidationError("Product Variant Id must be greater than zero");
            
            var variant = await _dbContext.ProductVariants.FirstOrDefaultAsync(p => p.Id == request.VariantId, cancellationToken);

            if (variant == null)
            {
                return BaseResponse<Unit>.NotFound($"Product Variant by id: {request.VariantId} not found");
            }
            
            var validationResult = await _validator.ValidateAsync(request.VariantDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return BaseResponse<Unit>.ValidationError(errors);
            }
            
            variant.Update(
                request.VariantDto.Stock,
                request.VariantDto.PriceOverride,
                request.VariantDto.ProductId,
                request.VariantDto.ColorId,
                request.VariantDto.SizeId
            );
            
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return BaseResponse<Unit>.Success("Product Variant updated successfully");
        }
        catch (Exception e)
        {
            return BaseResponse<Unit>.InternalError($"Unexpected error: {e.Message}");
        }
    }
}