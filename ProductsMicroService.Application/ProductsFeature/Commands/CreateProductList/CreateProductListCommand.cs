using MediatR;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.ProductsFeature.Commands.CreateProductList;

public record CreateProductListCommand(IEnumerable<CreateProductDto> ProductsDto): ICommand<BaseResponse<Unit>>;