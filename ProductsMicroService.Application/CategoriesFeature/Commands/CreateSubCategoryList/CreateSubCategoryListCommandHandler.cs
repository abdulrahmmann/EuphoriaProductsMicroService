using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.CategoriesFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Domain.Entities;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.CategoriesFeature.Commands.CreateSubCategoryList;

public class CreateSubCategoryListCommandHandler: ICommandHandler<CreateSubCategoryListCommand, BaseResponse<Unit>>
{
    #region Field Instance
    private readonly ApplicationDbContext  _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateSubCategoryDto>  _validator;
    #endregion

    #region Constructor
    public CreateSubCategoryListCommandHandler(ApplicationDbContext dbContext, IMapper mapper, IValidator<CreateSubCategoryDto> validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }
    #endregion
    
    public async Task<BaseResponse<Unit>> Handle(CreateSubCategoryListCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.SubCategories == null || !request.SubCategories.Any())
                return BaseResponse<Unit>.ValidationError("No main categories provided.");

            // Validate each DTO
            foreach (var dto in request.SubCategories)
            {
                var result = await _validator.ValidateAsync(dto, cancellationToken);
                if (!result.IsValid)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return BaseResponse<Unit>.ValidationError($"Main Category '{dto.Name}' is invalid: {errors}");
                }
            }

            // Filter out duplicates (either in DB or request itself)
            var existingNames = await _dbContext.SubCategories
                .Where(b => request.SubCategories.Select(x => x.Name).Contains(b.Name))
                .Select(b => b.Name)
                .ToListAsync(cancellationToken);

            var duplicates = request.SubCategories
                .Where(b => existingNames.Contains(b.Name))
                .Select(b => b.Name)
                .ToList();
            
            if (duplicates.Any())
            {
                var dupNames = string.Join(", ", duplicates);
                return BaseResponse<Unit>.Conflict($"The following main categories already exist: {dupNames}");
            }

            // Map + create entities
            var newCategories = request.SubCategories.Select(dto => SubCategory.Create(dto.Name, dto.Description, dto.CategoryId)).ToList();
            
            await _dbContext.SubCategories.AddRangeAsync(newCategories, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // 4️⃣ Success response
            var createdNames = string.Join(", ", newCategories.Select(b => b.Name));
            return BaseResponse<Unit>.Created($"Main Categories created successfully: {createdNames}");
        }
        catch (Exception e)
        {
            return BaseResponse<Unit>.InternalError($"Unexpected error: {e.Message}");
        }
    }
}