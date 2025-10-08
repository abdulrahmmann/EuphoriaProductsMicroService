using MediatR;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.ProductsFeature.Commands.CreateProduct;

public record CreateProductCommand(CreateProductDto ProductDto): ICommand<BaseResponse<Unit>>;