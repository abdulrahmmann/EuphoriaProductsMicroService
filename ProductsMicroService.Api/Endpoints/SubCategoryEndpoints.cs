using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductsMicroService.Application.CategoriesFeature.Commands.CreateSubCategoryList;
using ProductsMicroService.Application.CategoriesFeature.Queries.GetSubCategories;
using ProductsMicroService.Helpers;

namespace ProductsMicroService.Endpoints;

public static class SubCategoryEndpoints
{
    public static void MapSubCategoryEndpoints(this IEndpointRouteBuilder builder)
    {
        var endpoints = builder.MapGroup("/api/sub-categories")
            .WithTags("SubCategories")
            .WithOpenApi();
        
        // Create multiple SubCategories
        endpoints.MapPost("/create-list", async ([FromBody] CreateSubCategoryListCommand command, IMediator mediator) =>
            {
                var response = await mediator.Send(command);
                return response.ToResult();
            })
            .WithName("CreateSubCategoryList")
            .WithSummary("Create multiple sub categories.")
            .WithDescription("Creates a batch of sub categories in one request.");
        
        endpoints.MapGet("/list", async ([AsParameters] GetSubCategoriesQuery query, IMediator mediator) =>
            {
                var response = await mediator.Send(query);
                return response.ToResult();
            })
            .WithName("GetSubCategories")
            .WithSummary("Get all sub categories.")
            .WithDescription("Retrieves all sub category records.");
    }
}