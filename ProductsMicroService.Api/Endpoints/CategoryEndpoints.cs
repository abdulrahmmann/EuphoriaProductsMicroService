using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductsMicroService.Application.CategoriesFeature.Commands.CreateCategoryList;
using ProductsMicroService.Application.CategoriesFeature.Queries.GetCategories;
using ProductsMicroService.Application.CategoriesFeature.Queries.GetCategoriesWithSubCategories;
using ProductsMicroService.Helpers;

namespace ProductsMicroService.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder builder)
    {
        var endpoints = builder.MapGroup("/api/categories")
            .WithTags("Categories")
            .WithOpenApi();
        
        // Create multiple Categories
        endpoints.MapPost("/create-list", async ([FromBody] CreateCategoryListCommand command, IMediator mediator) =>
            {
                var response = await mediator.Send(command);
                return response.ToResult();
            })
            .WithName("CreateCategoryList")
            .WithSummary("Create multiple categories.")
            .WithDescription("Creates a batch of categories in one request.");
        
        
        endpoints.MapGet("/list", async ([AsParameters] GetCategoriesQuery query, IMediator mediator) =>
            {
                var response = await mediator.Send(query);
                return response.ToResult();
            })
            .WithName("GetCategories")
            .WithSummary("Get all categories.")
            .WithDescription("Retrieves all category records.");
        
        endpoints.MapGet("/category-subcategory-list", async ([AsParameters] GetCategoriesWithSubCategoriesQuery query, IMediator mediator) =>
            {
                var response = await mediator.Send(query);
                return response.ToResult();
            })
            .WithName("GetCategoriesWithSubCategories")
            .WithSummary("Get all categories with sub categories.")
            .WithDescription("Retrieves all category with sub categories records.");
        
    }
}