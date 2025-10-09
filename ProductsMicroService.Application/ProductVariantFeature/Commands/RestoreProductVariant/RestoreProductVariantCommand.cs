using MediatR;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.ProductVariantFeature.Commands.RestoreProductVariant;

public record RestoreProductVariantCommand(int  VariantId) : ICommand<BaseResponse<Unit>>;