using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.BrandFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Domain.Entities;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.BrandFeature.Commands.CreateBrandList;

public class CreateBrandListCommandHandler: ICommandHandler<CreateBrandListCommand, BaseResponse<Unit>>
{
    #region Field Instance
    private readonly ApplicationDbContext  _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateBrandDto>  _validator;
    #endregion

    #region Constructor
    public CreateBrandListCommandHandler(ApplicationDbContext dbContext, IMapper mapper, IValidator<CreateBrandDto> validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }
    #endregion
    
    public async Task<BaseResponse<Unit>> Handle(CreateBrandListCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Brands == null || !request.Brands.Any())
                return BaseResponse<Unit>.ValidationError("No brands provided.");

            // 1️⃣ Validate each DTO
            foreach (var dto in request.Brands)
            {
                var result = await _validator.ValidateAsync(dto, cancellationToken);
                if (!result.IsValid)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return BaseResponse<Unit>.ValidationError($"Brand '{dto.Name}' is invalid: {errors}");
                }
            }

            // 2️⃣ Filter out duplicates (either in DB or request itself)
            var existingNames = await _dbContext.Brands
                .Where(b => request.Brands.Select(x => x.Name).Contains(b.Name))
                .Select(b => b.Name)
                .ToListAsync(cancellationToken);

            var duplicates = request.Brands
                .Where(b => existingNames.Contains(b.Name))
                .Select(b => b.Name)
                .ToList();

            if (duplicates.Any())
            {
                var dupNames = string.Join(", ", duplicates);
                return BaseResponse<Unit>.Conflict($"The following brands already exist: {dupNames}");
            }

            // 3️⃣ Map + create entities
            var newBrands = request.Brands
                .Select(dto => Brand.CreateBrand(dto.Name))
                .ToList();

            await _dbContext.Brands.AddRangeAsync(newBrands, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // 4️⃣ Success response
            var createdNames = string.Join(", ", newBrands.Select(b => b.Name));
            return BaseResponse<Unit>.Created($"Brands created successfully: {createdNames}");
        }
        catch (Exception e)
        {
            return BaseResponse<Unit>.InternalError($"Unexpected error: {e.Message}");
        }
    }
}