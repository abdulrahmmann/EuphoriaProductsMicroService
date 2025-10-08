using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductsMicroService.Application.ProductsFeature.Commands.CreateProduct;
using ProductsMicroService.Application.ProductsFeature.Commands.CreateProductList;
using ProductsMicroService.Application.ProductsFeature.Commands.UpdateProduct;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Application.ProductsFeature.Queries.GetProducts;
using ProductsMicroService.Helpers;
using Scalar.AspNetCore;

namespace ProductsMicroService.Endpoints;

public static class ProductsEndpoints
{
    public static void MapProductsEndpoints(this IEndpointRouteBuilder builder)
    {
        var endpoints = builder.MapGroup("/api/products").WithTags("Products").WithOpenApi();

        #region Create Single Product Endpoint
        endpoints.MapPost("/create", async ([FromBody] CreateProductCommand command, IMediator mediator) =>
        {
            var response = await mediator.Send(command);
            return response.ToResult();
        })
        .WithName("CreateProduct")
        .WithSummary("Create Single Product.")
        .WithDescription("Creates a single product.")
        .WithBadge("CreateProductBadge");
        #endregion

        #region Create Multiple Products Endpoint
        endpoints.MapPost("/create-list", async ([FromBody] CreateProductListCommand command, IMediator mediator) =>
            {
                var response = await mediator.Send(command);
                return response.ToResult();
            })
            .WithName("CreateProductList")
            .WithSummary("Create List Product.")
            .WithDescription("Creates a List product.")
            .WithBadge("CreateProductListBadge");
        #endregion

        #region Update Product Endpoint
        endpoints.MapPut("/update/{productId}", async (int productId, [FromBody] UpdateProductDto dto, IMediator mediator) =>
            {
                var command = new UpdateProductCommand(productId, dto);
                var response = await mediator.Send(command);
                return response.ToResult();
            })
            .WithName("UpdateProduct")
            .WithSummary("Update Single Product.")
            .WithDescription("Update a single product.")
            .WithBadge("UpdateProductBadge");
        #endregion
        
        #region Get All Products Endpoint
        endpoints.MapGet("/list", async ([AsParameters] GetProductsQuery query, IMediator mediator) =>
            {
                var response = await mediator.Send(query);
                return response.ToResult();
            })
            .WithName("GetProducts")
            .WithSummary("Get All Products.")
            .WithDescription("Get All Products.")
            .WithBadge("GetProductsBadge");
        #endregion
    }
}