using MediatR;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductVariantFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.ProductVariantFeature.Commands.UpdateProductVariant;

public record UpdateProductVariantCommand(int VariantId, UpdateProductVariantDto VariantDto) 
    : ICommand<BaseResponse<Unit>>;