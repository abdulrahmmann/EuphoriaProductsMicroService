using Mapster;
using ProductsMicroService.Application.CategoriesFeature.DTOs;
using ProductsMicroService.Domain.Entities;

namespace ProductsMicroService.Application.Mapping;

public class CategoryMapping: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // ==============================
        // 🔹 Category Mappings
        // ==============================
        config.NewConfig<Category, CategoryDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description);

        config.NewConfig<CreateCategoryDto, Category>()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Ignore(dest => dest.SubCategories); 


        // ==============================
        // 🔹 SubCategory Mappings
        // ==============================
        config.NewConfig<SubCategory, SubCategoryDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.CategoryId, src => src.CategoryId)
            .Map(dest => dest.CategoryName, src => src.Category.Name);

        config.NewConfig<CreateSubCategoryDto, SubCategory>()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.CategoryId, src => src.CategoryId);
    }
}