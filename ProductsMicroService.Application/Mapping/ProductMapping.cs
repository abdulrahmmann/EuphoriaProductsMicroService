using Mapster;
using ProductsMicroService.Application.ProductsFeature.DTOs;
using ProductsMicroService.Domain.Entities;

namespace ProductsMicroService.Application.Mapping;

public class ProductMapping: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // -------------------------------
        // Entity → ProductDto
        // -------------------------------
        config.NewConfig<Product, ProductDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.TotalStock, src => src.TotalStock)
            .Map(dest => dest.ProductImages, src => src.ProductImages.ToList())
            .Map(dest => dest.BrandId, src => src.BrandId)
            .Map(dest => dest.BrandName, src => src.Brand.Name)
            .Map(dest => dest.CategoryId, src => src.CategoryId)
            .Map(dest => dest.CategoryName, src => src.Category.Name)
            .Map(dest => dest.SubCategoryId, src => src.SubCategoryId)
            .Map(dest => dest.SubCategoryName, src => src.SubCategory.Name);
        
        // -------------------------------
        // CreateProductDto → Product
        // -------------------------------
        config.NewConfig<CreateProductDto, Product>()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.TotalStock, src => src.TotalStock)
            .Map(dest => dest.BrandId, src => src.BrandId)
            .Map(dest => dest.CategoryId, src => src.CategoryId)
            .Map(dest => dest.SubCategoryId, src => src.SubCategoryId)
            .AfterMapping((src, dest) =>
            {
                dest.ProductImages.Clear();
                foreach (var img in src.ProductImages.Where(i => !string.IsNullOrWhiteSpace(i)))
                    dest.ProductImages.Add(img);
            });

        // -------------------------------
        // UpdateProductDto → Product
        // -------------------------------
        config.NewConfig<UpdateProductDto, Product>()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.TotalStock, src => src.TotalStock)
            .Map(dest => dest.ProductImages, src => src.ProductImages
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Cast<string>()
                .ToList())
            .Map(dest => dest.BrandId, src => src.BrandId)
            .Map(dest => dest.CategoryId, src => src.CategoryId)
            .Map(dest => dest.SubCategoryId, src => src.SubCategoryId);
    }
}