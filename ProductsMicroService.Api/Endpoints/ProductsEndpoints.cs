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
        
        // ======================== 🟢 CREATE 🟢 ======================

        #region Create Single Product
        endpoints.MapPost("/create", async ([FromBody] CreateProductCommand command, IMediator mediator) =>
            {
                var response = await mediator.Send(command);
                return response.ToResult();
            })
            .WithName("CreateProduct")
            .WithSummary("Create a single product.")
            .WithDescription("Creates a new product with its associated brand, category, and subcategory.")
            .WithBadge("CreateProductBadge");
        #endregion

        #region Create Multiple Products
        endpoints.MapPost("/create-list", async ([FromBody] CreateProductListCommand command, IMediator mediator) =>
            {
                var response = await mediator.Send(command);
                return response.ToResult();
            })
            .WithName("CreateProductList")
            .WithSummary("Create multiple products.")
            .WithDescription("Creates a list of products in a single request.")
            .WithBadge("CreateProductListBadge");
        #endregion

        // ====================== 🟡 UPDATE & RESTORE 🟡 ===============

        #region Update Product
        endpoints.MapPut("/update/{productId:int}", async (int productId, [FromBody] UpdateProductDto dto, IMediator mediator) =>
            {
                var command = new UpdateProductCommand(productId, dto);
                var response = await mediator.Send(command);
                return response.ToResult();
            })
            .WithName("UpdateProduct")
            .WithSummary("Update an existing product.")
            .WithDescription("Updates product details including name, price, brand, and category.")
            .WithBadge("UpdateProductBadge");
        #endregion

        #region Restore Product
        endpoints.MapPut("/restore/{productId:int}", async (int productId, IMediator mediator) =>
            {
                var response = await mediator.Send(new RestoreProductCommand(productId));
                return response.ToResult();
            })
            .WithName("RestoreProduct")
            .WithSummary("Restore a soft-deleted product.")
            .WithDescription("Restores a previously soft-deleted product by ID.")
            .WithBadge("RestoreProductBadge");
        #endregion

        // ======================== 🔴 DELETE 🔴 ========================

        #region Delete Product
        endpoints.MapDelete("/delete/{productId:int}", async (int productId, IMediator mediator) =>
            {
                var response = await mediator.Send(new DeleteProductCommand(productId));
                return response.ToResult();
            })
            .WithName("DeleteProduct")
            .WithSummary("Delete a product by ID.")
            .WithDescription("Soft deletes a single product by its unique identifier.")
            .WithBadge("DeleteProductBadge");
        #endregion

        // ======================== 🔍 QUERIES 🔍 ========================

        #region Get All Products
        endpoints.MapGet("/list", async (IMediator mediator) =>
            {
                var response = await mediator.Send(new GetProductsQuery());
                return response.ToResult();
            })
            .WithName("GetProducts")
            .WithSummary("Retrieve all products.")
            .WithDescription("Returns all non-deleted products with their brand, category, and stock information.")
            .WithBadge("GetProductsBadge")
            .Produces<BaseResponse<IEnumerable<ProductDto>>>(StatusCodes.Status200OK)
            .Produces<BaseResponse<string>>(StatusCodes.Status500InternalServerError);
        #endregion

        #region Get Product By ID
        endpoints.MapGet("/{productId:int}", async (int productId, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetProductByIdQuery(productId));
                return response.ToResult();
            })
            .WithName("GetProductById")
            .WithSummary("Get a product by ID.")
            .WithDescription("Fetches detailed information for a product including brand, category, and subcategory names.")
            .WithBadge("GetProductByIdBadge");
        #endregion

        #region Get Products By Price Range
        endpoints.MapGet("/price-range", async ([FromQuery] decimal min, [FromQuery] decimal max, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetProductsByPriceRangeQuery(min, max));
                return response.ToResult();
            })
            .WithName("GetProductsByPriceRange")
            .WithSummary("Get products filtered by price range.")
            .WithDescription("Returns all products whose prices fall within the specified minimum and maximum values.")
            .WithBadge("PriceRangeBadge");
        #endregion

        #region Search Products By Name
        endpoints.MapGet("/search", async ([FromQuery] string name, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetProductByNameQuery(name));
                return response.ToResult();
            })
            .WithName("SearchProductByName")
            .WithSummary("Search products by name.")
            .WithDescription("Searches for products matching part or full name, case-insensitive.")
            .WithBadge("SearchProductBadge");
        #endregion

        #region Get Products With Variants

        endpoints.MapGet("/with-variants", async (IMediator mediator) =>
            {
                var response = await mediator.Send(new GetProductsWithVariantsQuery());
                return response.ToResult();
            })
            .WithName("GetProductsWithVariants")
            .WithSummary("Get all products with their variants.")
            .WithDescription("Returns all non-deleted products with their brand, category, subcategory, and variant details (using projection joins).")
            .WithBadge("GetProductsWithVariantsBadge");

        #endregion
    }
}