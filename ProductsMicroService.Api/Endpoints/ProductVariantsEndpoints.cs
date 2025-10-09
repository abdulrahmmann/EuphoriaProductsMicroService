using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductsMicroService.Application.Bases;
using ProductsMicroService.Application.ProductsFeature.Queries.GetProductsWithVariant;
using ProductsMicroService.Application.ProductVariantFeature.Commands.CreateProductVariant;
using ProductsMicroService.Application.ProductVariantFeature.Commands.CreateProductVariantList;
using ProductsMicroService.Application.ProductVariantFeature.Commands.DeleteProductVariant;
using ProductsMicroService.Application.ProductVariantFeature.Commands.RestoreProductVariant;
using ProductsMicroService.Application.ProductVariantFeature.Commands.UpdateProductVariant;
using ProductsMicroService.Application.ProductVariantFeature.DTOs;
using ProductsMicroService.Application.ProductVariantFeature.Queries.GetAllWithDetails;
using ProductsMicroService.Helpers;
using Scalar.AspNetCore;

namespace ProductsMicroService.Endpoints;

public static class ProductVariantsEndpoints
{
    public static void MapProductVariantsEndpoints(this IEndpointRouteBuilder builder)
    {
        var endpoints = builder.MapGroup("/api/product-variants").WithTags("ProductsVariants").WithOpenApi();
        
        // ======================== 🟢 CREATE 🟢 ======================

        #region Create Single Product Variant
        endpoints.MapPost("/create", async ([FromBody] CreateProductVariantCommand command, IMediator mediator) =>
            {
                var response = await mediator.Send(command);
                return response.ToResult();
            })
            .WithName("CreateProductVariant")
            .WithSummary("Create a single product variant.")
            .WithDescription("Creates a new product variant linked to a specific product, color, and size.")
            .WithBadge("CreateProductVariantBadge");
        #endregion

        #region Create Multiple Product Variants
        endpoints.MapPost("/create-list", async ([FromBody] CreateProductVariantListCommand command, IMediator mediator) =>
            {
                var response = await mediator.Send(command);
                return response.ToResult();
            })
            .WithName("CreateProductVariantList")
            .WithSummary("Create multiple product variants.")
            .WithDescription("Creates a list of product variants in a single request.")
            .WithBadge("CreateProductVariantListBadge");
        #endregion

        // ====================== 🟡 UPDATE & RESTORE 🟡 ===============

        #region Update Product Variant
        endpoints.MapPut("/update/{productVariantId:int}", async (int productVariantId, [FromBody] UpdateProductVariantDto dto, IMediator mediator) =>
            {
                var command = new UpdateProductVariantCommand(productVariantId, dto);
                var response = await mediator.Send(command);
                return response.ToResult();
            })
            .WithName("UpdateProductVariant")
            .WithSummary("Update an existing product variant.")
            .WithDescription("Updates product variant details including stock, price override, color, size, or linked product.")
            .WithBadge("UpdateProductVariantBadge");
        #endregion

        #region Restore Product Variant
        endpoints.MapPut("/restore/{productVariantId:int}", async (int productVariantId, IMediator mediator) =>
            {
                var response = await mediator.Send(new RestoreProductVariantCommand(productVariantId));
                return response.ToResult();
            })
            .WithName("RestoreProductVariant")
            .WithSummary("Restore a soft-deleted product variant.")
            .WithDescription("Restores a previously soft-deleted product variant by its ID.")
            .WithBadge("RestoreProductVariantBadge");
        #endregion

        // ======================== 🔴 DELETE 🔴 ========================

        #region Delete Product Variant
        endpoints.MapDelete("/delete/{productVariantId:int}", async (int productVariantId, IMediator mediator) =>
            {
                var response = await mediator.Send(new DeleteProductVariantCommand(productVariantId));
                return response.ToResult();
            })
            .WithName("DeleteProductVariant")
            .WithSummary("Delete a product variant by ID.")
            .WithDescription("Soft deletes a single product variant by its unique identifier.")
            .WithBadge("DeleteProductVariantBadge");
        #endregion

        // ======================== 🔍 QUERIES 🔍 ========================

        #region Get All Product Variants
        endpoints.MapGet("/list", async (IMediator mediator) =>
            {
                var response = await mediator.Send(new GetProductVariantsQuery());
                return response.ToResult();
            })
            .WithName("GetProductVariants")
            .WithSummary("Retrieve all product variants.")
            .WithDescription("Returns all non-deleted product variants with their product, color, and size details.")
            .WithBadge("GetProductVariantsBadge")
            .Produces<BaseResponse<IEnumerable<ProductVariantDto>>>(StatusCodes.Status200OK)
            .Produces<BaseResponse<string>>(StatusCodes.Status500InternalServerError);
        #endregion
    }
}