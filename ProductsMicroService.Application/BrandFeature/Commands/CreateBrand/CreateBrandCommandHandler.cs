using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.BrandFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Domain.Entities;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.BrandFeature.Commands.CreateBrand;

public class CreateBrandCommandHandler: IQueryHandler<CreateBrandCommand, BaseResponse<Unit>>
{
    #region Field Instance
    private readonly ApplicationDbContext  _dbContext;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateBrandDto>  _validator;
    #endregion

    #region Constructor
    public CreateBrandCommandHandler(ApplicationDbContext dbContext, IMapper mapper, IValidator<CreateBrandDto> validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }
    #endregion


    public async Task<BaseResponse<Unit>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Check Validation.
            var validationResult = await _validator.ValidateAsync(request.BrandDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return BaseResponse<Unit>.ValidationError(errors);
            }

            // 2. Check Existing Brand.
            var isExist = await _dbContext.Brands.FirstOrDefaultAsync(b => b.Name == request.BrandDto.Name, cancellationToken);

            if (isExist is not null)
            {
                return BaseResponse<Unit>.Conflict();
            }
            
            // 3. Create Brand.
            var newBrand = Brand.Create(request.BrandDto.Name);
            
            // 4. Map Brand.
            var brandMapped = _mapper.Map<CreateBrandDto>(newBrand);
            
            await _dbContext.AddAsync(newBrand, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return BaseResponse<Unit>.Created($"Brand with name: {brandMapped.Name}  has been successfully created");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<Unit>.ValidationError(e.Message);
        }
    }
}