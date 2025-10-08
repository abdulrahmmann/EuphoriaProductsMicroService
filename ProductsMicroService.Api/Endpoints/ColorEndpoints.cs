using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductsMicroService.Application.ColorsFeature.Commands.CreateColorList;
using ProductsMicroService.Application.ColorsFeature.Queries.GetColors;
using ProductsMicroService.Helpers;

namespace ProductsMicroService.Endpoints;

public static class ColorEndpoints
{
    public static void MapColorEndpoints(this IEndpointRouteBuilder builder)
    {
        var endpoints = builder.MapGroup("/api/colors")
            .WithTags("Colors")
            .WithOpenApi();
        
        endpoints.MapPost("/create-list", async ([FromBody] CreateColorListCommand command, IMediator mediator) =>
            {
                var response = await mediator.Send(command);
                return response.ToResult();
            })
            .WithName("CreateColorList")
            .WithSummary("Create multiple colors.")
            .WithDescription("Creates a batch of colors in one request.");
        
        endpoints.MapGet("/list", async ([AsParameters] GetColorsQuery query, IMediator mediator) =>
            {
                var response = await mediator.Send(query);
                return response.ToResult();
            })
            .WithName("GetColors")
            .WithSummary("Get all colors.")
            .WithDescription("Retrieves all color records.");
    }
}