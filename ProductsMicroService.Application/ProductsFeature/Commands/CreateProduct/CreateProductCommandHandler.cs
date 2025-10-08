using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;
using ProductsMicroService.Domain.Entities;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Application.ProductsFeature.Commands.CreateProduct;

public class CreateProductCommandHandler: ICommandHandler<CreateProductCommand, BaseResponse<Unit>>
{
    private readonly ApplicationDbContext  _dbContext;
    private readonly IValidator<CreateProductDto>  _validator;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(ApplicationDbContext dbContext, IMapper mapper, IValidator<CreateProductDto>  validator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<BaseResponse<Unit>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request.ProductDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = string.Join(",", validationResult.Errors.Select(err => err.ErrorMessage));
                return BaseResponse<Unit>.ValidationError(errors);
            }
            
            var exists = await _dbContext.Products
                .AnyAsync(p =>
                        p.Name.ToLower() == request.ProductDto.Name.ToLower().Trim() &&
                        p.BrandId == request.ProductDto.BrandId &&
                        p.CategoryId == request.ProductDto.CategoryId &&
                        p.SubCategoryId == request.ProductDto.SubCategoryId,
                    cancellationToken);

            if (exists)
            {
                return BaseResponse<Unit>.Conflict($"Product with name: {request.ProductDto.Name} already exists!");
            }

            var req = request.ProductDto;
            
            var product = Product.Create(req.Name, req.Description, req.Price, req.TotalStock, 
                req.ProductImages, req.CategoryId, req.SubCategoryId, req.BrandId);
            
            
            await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return BaseResponse<Unit>.Created("Products has been created successfully");
        }
        catch (Exception e)
        {
            return BaseResponse<Unit>.InternalError($"Unexpected error: {e.Message}");
        }
    }
}