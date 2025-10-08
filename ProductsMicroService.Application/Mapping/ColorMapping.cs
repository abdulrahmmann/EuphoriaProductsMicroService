using Mapster;
using ProductsMicroService.Application.ColorsFeature.DTOs;
using ProductsMicroService.Domain.Entities;

namespace ProductsMicroService.Application.Mapping;

public class ColorMapping: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Color, ColorDto>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.HexCode, src => src.HexCode);
    }
}