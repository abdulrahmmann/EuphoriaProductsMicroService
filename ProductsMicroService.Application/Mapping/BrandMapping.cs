using Mapster;
using ProductsMicroService.Application.BrandFeature.DTOs;
using ProductsMicroService.Domain.Entities;

namespace ProductsMicroService.Application.Mapping;

public class BrandMapping: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Brand, BrandDto>()
            .Map(dest => dest.Name, src => src.Name);
        
        config.NewConfig<Brand, CreateBrandDto>()
            .Map(dest => dest.Name, src => src.Name);
        
        config.NewConfig<Brand, UpdateBrandDto>()
            .Map(dest => dest.Name, src => src.Name);
    }
}