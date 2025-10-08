using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Domain.Entities;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.ProductsFeature.Commands.CreateProductList;

public class CreateProductListHandler: ICommandHandler<CreateProductListCommand, BaseResponse<Unit>>
{
    #region Create and Inject Instances.
    private readonly ApplicationDbContext  _dbContext;
    private readonly IValidator<CreateProductDto>  _validator;
    private readonly IMapper _mapper;
    
    public CreateProductListHandler(ApplicationDbContext dbContext, IMapper mapper, IValidator<CreateProductDto>  validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }
    #endregion
    
    public async Task<BaseResponse<Unit>> Handle(CreateProductListCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.ProductsDto == null || !request.ProductsDto.Any())
                return BaseResponse<Unit>.ValidationError("No products provided.");

            // Validate each DTO
            foreach (var dto in request.ProductsDto)
            {
                var result = await _validator.ValidateAsync(dto, cancellationToken);
                if (!result.IsValid)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return BaseResponse<Unit>.ValidationError($"product: '{dto.Name}' is invalid: {errors}");
                }
            }

            // Filter out duplicates (either in DB or request itself)
            var existingNames = await _dbContext.Products
                .Where(b => request.ProductsDto.Select(x => x.Name).Contains(b.Name))
                .Select(b => b.Name)
                .ToListAsync(cancellationToken);

            var duplicates = request.ProductsDto
                .Where(b => existingNames.Contains(b.Name))
                .Select(b => b.Name)
                .ToList();
            
            if (duplicates.Any())
            {
                var dupNames = string.Join(", ", duplicates);
                return BaseResponse<Unit>.Conflict($"The following products already exist: {dupNames}");
            }

            // Map + create entities
            var newProducts = request.ProductsDto
                .Select(dto => Product.Create(
                    dto.Name,
                    dto.Description,
                    dto.Price,
                    dto.TotalStock,
                    dto.ProductImages,
                    dto.CategoryId,
                    dto.SubCategoryId,
                    dto.BrandId
                    )).ToList();
            
            await _dbContext.Products.AddRangeAsync(newProducts, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // 4️⃣ Success response
            var createdNames = string.Join(", ", newProducts.Select(b => b.Name));
            return BaseResponse<Unit>.Created($"Products created successfully: {createdNames}");
        }
        catch (Exception e)
        {
            return BaseResponse<Unit>.InternalError($"Unexpected error: {e.Message}");
        }
    }
}