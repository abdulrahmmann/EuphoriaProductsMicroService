using MediatR;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductVariantFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.ProductVariantFeature.Commands.CreateProductVariant;

public record CreateProductVariantCommand(CreateProductVariantDto VariantDto): ICommand<BaseResponse<Unit>>;