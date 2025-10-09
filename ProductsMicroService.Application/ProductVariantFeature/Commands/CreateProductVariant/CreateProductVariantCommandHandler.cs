using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductVariantFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Domain.Entities;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.ProductVariantFeature.Commands.CreateProductVariant;

public class CreateProductVariantCommandHandler: ICommandHandler<CreateProductVariantCommand, BaseResponse<Unit>>
{
    #region Create and Inject Instances.
    private readonly ApplicationDbContext  _dbContext;
    private readonly IValidator<CreateProductVariantDto>  _validator;
    private readonly IMapper _mapper;
    
    public CreateProductVariantCommandHandler(ApplicationDbContext dbContext, IMapper mapper, IValidator<CreateProductVariantDto>  validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }
    #endregion
    
    public async Task<BaseResponse<Unit>> Handle(CreateProductVariantCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request.VariantDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(",", validationResult.Errors.Select(err => err.ErrorMessage));
                return BaseResponse<Unit>.ValidationError(errors);
            }
            
            var req = request.VariantDto;
            
            var exists = await _dbContext.ProductVariants
                .AnyAsync(p =>
                        p.PriceOverride == req.PriceOverride &&
                        p.ColorId == req.ColorId &&
                        p.ProductId == req.ProductId &&
                        p.SizeId == req.SizeId &&
                        p.Stock == req.Stock,
                    cancellationToken);

            if (exists)
            {
                return BaseResponse<Unit>.Conflict($"Product Variant with ProductId: {req.ProductId} already exists!");
            }
            
            var variant = ProductVariant.Create(req.ProductId, req.ColorId, req.SizeId, 
                req.Stock, req.PriceOverride);
            
            
            await _dbContext.ProductVariants.AddAsync(variant, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return BaseResponse<Unit>.Created("ProductVariants has been created successfully");
        }
        catch (Exception e)
        {
            return BaseResponse<Unit>.InternalError($"Unexpected error: {e.Message}");
        }
    }
}