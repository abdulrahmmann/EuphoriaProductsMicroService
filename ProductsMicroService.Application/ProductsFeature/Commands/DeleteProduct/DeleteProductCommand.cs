using MediatR;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.ProductsFeature.Commands.DeleteProduct;

public record DeleteProductCommand(int ProductId): ICommand<BaseResponse<Unit>>;