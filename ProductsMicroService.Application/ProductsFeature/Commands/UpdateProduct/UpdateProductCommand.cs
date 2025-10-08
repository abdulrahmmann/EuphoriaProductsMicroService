using MediatR;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.ProductsFeature.Commands.UpdateProduct;

public record UpdateProductCommand(int ProductId, UpdateProductDto ProductDto): ICommand<BaseResponse<Unit>>;