using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.Commands.CreateProduct;
using ProductsMicroService.Application.ProductsFeature.Commands.CreateProductList;
using ProductsMicroService.Application.ProductsFeature.Commands.DeleteProduct;
using ProductsMicroService.Application.ProductsFeature.Commands.RestoreProduct;
using ProductsMicroService.Application.ProductsFeature.Commands.UpdateProduct;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Application.ProductsFeature.Queries.GetProductById;
using ProductsMicroService.Application.ProductsFeature.Queries.GetProducts;
using ProductsMicroService.Application.ProductsFeature.Queries.GetProductsByPriceRange;
using ProductsMicroService.Application.ProductsFeature.Queries.GetProductsWithVariant;
using ProductsMicroService.Application.ProductsFeature.Queries.SearchProductByName;
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

        #region Restore Product Endpoint
        endpoints.MapPut("/restore/{productId}", async (int productId, IMediator mediator) =>
            {
                var response = await mediator.Send(new RestoreProductCommand(productId));
                return response.ToResult();
            })
            .WithName("RestoreProduct")
            .WithSummary("Restore a soft-deleted product.")
            .WithDescription("Restores a previously soft-deleted product by ID.")
            .WithBadge("RestoreProductBadge");
        #endregion

        #region Delete Product Endpoint
        endpoints.MapDelete("/delete/{productId}", async (int productId, IMediator mediator) =>
            {
                var response = await mediator.Send(new DeleteProductCommand(productId));
                return response.ToResult();
            })
            .WithName("DeleteProduct")
            .WithSummary("Delete Single Product.")
            .WithDescription("Delete a single product.")
            .WithBadge("DeleteProductBadge");
        #endregion
        
        
        #region Get All Products Endpoint
        endpoints.MapGet("/list", async (IMediator mediator) =>
            {
                var response = await mediator.Send(new GetProductsQuery());
                return response.ToResult();
            })
            .WithName("GetProducts")
            .WithSummary("Get All Products.")
            .WithDescription("Get All Products.")
            .WithBadge("GetProductsBadge");
        #endregion

        #region Get Products By Id
        endpoints.MapGet("/{productId}", async (int productId, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetProductByIdQuery(productId));
                return response.ToResult();
            })
            .WithName("GetProductById")
            .WithSummary("Get product by ID.")
            .WithDescription("Fetches product details with brand, category, and subcategory names.");
        #endregion
        
        #region Get Products By Price Range
        endpoints.MapGet("/price-range", async (decimal min, decimal max, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetProductsByPriceRangeQuery(min, max));
                return response.ToResult();
            })
            .WithName("GetProductsByPriceRange")
            .WithSummary("Get products filtered by price range.")
            .WithDescription("Returns products whose prices fall between the given minimum and maximum values.")
            .WithBadge("PriceRangeBadge");
        #endregion
        
        #region Search Product Endpoint
        endpoints.MapGet("/search", async (string name, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetProductByNameQuery(name));
                return response.ToResult();
            })
            .WithName("SearchProductByName")
            .WithSummary("Search products by name.")
            .WithDescription("Search for products matching part or full name, case-insensitive.")
            .WithBadge("SearchProductBadge");
        #endregion

        #region Products With Variants Endpoint
        endpoints.MapGet("/with-variants", async (IMediator mediator) =>
            {
                var response = await mediator.Send(new GetProductsWithVariantsQuery());
                return response.ToResult();
            })
            .WithName("GetProductsWithVariants")
            .WithSummary("Get all products with their variants")
            .WithDescription("Returns all non-deleted products with their Brand, Category, SubCategory, and Variant details (using projection joins).")
            .Produces<BaseResponse<IEnumerable<ProductWithVariantsDto>>>(StatusCodes.Status200OK)
            .Produces<BaseResponse<string>>(StatusCodes.Status500InternalServerError);
        #endregion

    }
}