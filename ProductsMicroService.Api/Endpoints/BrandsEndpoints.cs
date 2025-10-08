using MediatR;
using Microsoft.AspNetCore.Mvc; // ✅ needed for [FromBody]
using ProductsMicroService.Application.BrandFeature.Commands.CreateBrand;
using ProductsMicroService.Application.BrandFeature.Commands.CreateBrandList;
using ProductsMicroService.Application.BrandFeature.Queries.GetBrandById;
using ProductsMicroService.Application.BrandFeature.Queries.GetBrands;
using ProductsMicroService.Helpers;

namespace ProductsMicroService.Endpoints;

public static class BrandsEndpoints
{
    public static void MapBrandEndpoints(this IEndpointRouteBuilder builder)
    {
        var endpoints = builder.MapGroup("/api/brands")
            .WithTags("Brands")
            .WithOpenApi();

        // Create single brand
        endpoints.MapPost("/create", async ([FromBody] CreateBrandCommand command, IMediator mediator) =>
            {
                var response = await mediator.Send(command);
                return response.ToResult();
            })
            .WithName("CreateBrand")
            .WithSummary("Create a single brand.")
            .WithDescription("Validates input and creates a new brand record.");

        // Create multiple brands
        endpoints.MapPost("/create-list", async ([FromBody] CreateBrandListCommand command, IMediator mediator) =>
            {
                var response = await mediator.Send(command);
                return response.ToResult();
            })
            .WithName("CreateBrandList")
            .WithSummary("Create multiple brands.")
            .WithDescription("Creates a batch of brands in one request.");

        // Get brands — query binding still automatic
        endpoints.MapGet("/list", async ([AsParameters] GetBrandsQuery query, IMediator mediator) =>
            {
                var response = await mediator.Send(query);
                return response.ToResult();
            })
            .WithName("GetBrands")
            .WithSummary("Get all brands.")
            .WithDescription("Retrieves all brand records.");
        
        // Get brands by id
        endpoints.MapGet("/{id:int}", async (int id, IMediator mediator) =>
            {
                var query = new GetBrandByIdQuery(id);
                var response = await mediator.Send(query);
                return response.ToResult();
            })
            .WithName("GetBrandById")
            .WithSummary("Get brand by ID.")
            .WithDescription("Retrieves a single brand by its unique identifier.");
    }
}