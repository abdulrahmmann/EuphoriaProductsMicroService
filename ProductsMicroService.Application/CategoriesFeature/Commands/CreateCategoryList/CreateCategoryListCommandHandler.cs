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

namespace ProductsMicroService.Application.CategoriesFeature.Commands.CreateCategoryList;

public class CreateCategoryListCommandHandler: ICommandHandler<CreateCategoryListCommand, BaseResponse<Unit>>
{
    #region Field Instance
    private readonly ApplicationDbContext  _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCategoryDto>  _validator;
    #endregion

    #region Constructor
    public CreateCategoryListCommandHandler(ApplicationDbContext dbContext, IMapper mapper, IValidator<CreateCategoryDto> validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }
    #endregion
    
    public async Task<BaseResponse<Unit>> Handle(CreateCategoryListCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Categories == null || !request.Categories.Any())
                return BaseResponse<Unit>.ValidationError("No categories provided.");

            // Validate each DTO
            foreach (var dto in request.Categories)
            {
                var result = await _validator.ValidateAsync(dto, cancellationToken);
                if (!result.IsValid)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return BaseResponse<Unit>.ValidationError($"Category '{dto.Name}' is invalid: {errors}");
                }
            }

            // Filter out duplicates (either in DB or request itself)
            var existingNames = await _dbContext.Categories
                .Where(b => request.Categories.Select(x => x.Name).Contains(b.Name))
                .Select(b => b.Name)
                .ToListAsync(cancellationToken);

            var duplicates = request.Categories
                .Where(b => existingNames.Contains(b.Name))
                .Select(b => b.Name)
                .ToList();

            if (duplicates.Any())
            {
                var dupNames = string.Join(", ", duplicates);
                return BaseResponse<Unit>.Conflict($"The following categories already exist: {dupNames}");
            }

            // Map + create entities
            var newCategories = request.Categories.Select(dto => Category.CreateCategory(dto.Name, dto.Description)).ToList();

            await _dbContext.Categories.AddRangeAsync(newCategories, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // 4️⃣ Success response
            var createdNames = string.Join(", ", newCategories.Select(b => b.Name));
            return BaseResponse<Unit>.Created($"Categories created successfully: {createdNames}");
        }
        catch (Exception e)
        {
            return BaseResponse<Unit>.InternalError($"Unexpected error: {e.Message}");
        }
    }
}