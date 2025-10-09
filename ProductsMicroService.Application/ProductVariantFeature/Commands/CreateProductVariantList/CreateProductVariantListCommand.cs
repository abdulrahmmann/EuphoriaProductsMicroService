using MediatR;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductVariantFeature.DTOs;
using ProductsMicroService.Domain.CQRS;

namespace ProductsMicroService.Application.ProductVariantFeature.Commands.CreateProductVariantList;

public record CreateProductVariantListCommand(IEnumerable<CreateProductVariantDto> VariantsDto)
    : ICommand<BaseResponse<Unit>>;