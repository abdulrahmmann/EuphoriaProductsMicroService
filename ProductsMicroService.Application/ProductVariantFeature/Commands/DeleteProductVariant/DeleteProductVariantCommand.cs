using MediatR;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.ProductVariantFeature.Commands.DeleteProductVariant;

public record DeleteProductVariantCommand(int VariantId): ICommand<BaseResponse<Unit>>;