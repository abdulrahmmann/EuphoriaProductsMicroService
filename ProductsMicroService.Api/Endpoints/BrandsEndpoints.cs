using MediatR;
using ProductsMicroService.Application.BrandFeature.Commands.CreateBrand;
using ProductsMicroService.Application.BrandFeature.Commands.CreateBrandList;
using ProductsMicroService.Helpers;

namespace ProductsMicroService.Endpoints;

public static class BrandsEndpoints
{
    public static void MapBrandEndpoints(this IEndpointRouteBuilder builder)
    {
        var endpoints = builder.MapGroup("/api/brands")
            .WithTags("Brands")
            .WithOpenApi();

        endpoints.MapPost("/create", async (CreateBrandCommand command, IMediator mediator) =>
        {
            var response = await mediator.Send(command);
            return response.ToResult();
        })
        .WithName("CreateBrand")
        .WithSummary("Create a single brand.")
        .WithDescription("Validates input and creates a new brand record.");
        
        endpoints.MapPost("/create-list", async (CreateBrandListCommand command, IMediator mediator) =>
        {
            var response = await mediator.Send(command);
            return response.ToResult();
        })
        .WithName("CreateBrandList")
        .WithSummary("Create multiple brands.")
        .WithDescription("Creates a batch of brands in one request.");
    }
}