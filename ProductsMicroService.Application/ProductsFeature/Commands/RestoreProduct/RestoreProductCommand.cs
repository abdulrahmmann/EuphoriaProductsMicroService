using MediatR;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.ProductsFeature.Commands.RestoreProduct;

public record RestoreProductCommand(int ProductId): ICommand<BaseResponse<Unit>>;