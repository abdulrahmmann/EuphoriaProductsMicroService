using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ColorsFeature.DTOs;
using ProductsMicroService.Application.ColorsFeature.Validators;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Domain.Entities;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.ColorsFeature.Commands.CreateColorList;

public class CreateColorListCommandHandler: ICommandHandler<CreateColorListCommand, BaseResponse<Unit>>
{
    #region Field Instance
    private readonly ApplicationDbContext  _dbContext;
    private readonly IValidator<CreateColorDto>  _validator;
    #endregion

    #region Constructor
    public CreateColorListCommandHandler(ApplicationDbContext dbContext, IValidator<CreateColorDto> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(CreateColorListCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Colors == null || !request.Colors.Any())
                return BaseResponse<Unit>.ValidationError("No Colors provided.");

            // Validate each DTO
            foreach (var dto in request.Colors)
            {
                var result = await _validator.ValidateAsync(dto, cancellationToken);
                if (!result.IsValid)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return BaseResponse<Unit>.ValidationError($"Color '{dto.Name}' is invalid: {errors}");
                }
            }

            // Filter out duplicates (either in DB or request itself)
            var existingNames = await _dbContext.Colors
                .Where(b => request.Colors.Select(x => x.Name).Contains(b.Name))
                .Select(b => b.Name)
                .ToListAsync(cancellationToken);

            var duplicates = request.Colors
                .Where(b => existingNames.Contains(b.Name))
                .Select(b => b.Name)
                .ToList();

            if (duplicates.Any())
            {
                var dupNames = string.Join(", ", duplicates);
                return BaseResponse<Unit>.Conflict($"The following colors already exist: {dupNames}");
            }

            // Map + create entities
            var newColor = request.Colors
                .Select(dto => Color.Create(dto.Name, dto.HexCode))
                .ToList();

            await _dbContext.Colors.AddRangeAsync(newColor, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // 4️⃣ Success response
            var createdNames = string.Join(", ", newColor.Select(b => b.Name));
            return BaseResponse<Unit>.Created($"Colors created successfully: {createdNames}");
        }
        catch (Exception e)
        {
            return BaseResponse<Unit>.InternalError($"Unexpected error: {e.Message}");
        }
    }
}