using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductVariantFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Domain.Entities;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.ProductVariantFeature.Commands.CreateProductVariantList;

public class CreateProductVariantListCommandHandler: ICommandHandler<CreateProductVariantListCommand, BaseResponse<Unit>>
{
    #region Create and Inject Instances.
    private readonly ApplicationDbContext  _dbContext;
    private readonly IValidator<CreateProductVariantDto>  _validator;
    private readonly IMapper _mapper;
    
    public CreateProductVariantListCommandHandler(ApplicationDbContext dbContext, IMapper mapper, IValidator<CreateProductVariantDto>  validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(CreateProductVariantListCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.VariantsDto == null || !request.VariantsDto.Any())
                return BaseResponse<Unit>.ValidationError("No product variants provided.");

            // Validate each DTO
            foreach (var dto in request.VariantsDto)
            {
                var result = await _validator.ValidateAsync(dto, cancellationToken);
                if (!result.IsValid)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return BaseResponse<Unit>.ValidationError($"product variants: '{dto.ProductId}' is invalid: {errors}");
                }
            }

            // Map + create entities
            var newProducts = request.VariantsDto
                .Select(dto => ProductVariant.Create(
                    dto.ProductId,
                    dto.ColorId,
                    dto.SizeId,
                    dto.Stock,
                    dto.PriceOverride
                    )).ToList();
            
            await _dbContext.ProductVariants.AddRangeAsync(newProducts, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // 4️⃣ Success response
            var createdNames = string.Join(", ", newProducts.Select(b => b.ProductId));
            return BaseResponse<Unit>.Created($"Product Variants created successfully: {createdNames}");
        }
        catch (Exception e)
        {
            return BaseResponse<Unit>.InternalError($"Unexpected error: {e.Message}");
        }
    }
}