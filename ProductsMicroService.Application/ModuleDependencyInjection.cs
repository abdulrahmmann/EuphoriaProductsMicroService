using System.Reflection;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsMicroService.Application.BrandFeature.Validators;
using ProductsMicroService.Application.CategoriesFeature.Validators;
using ProductsMicroService.Application.ColorsFeature.Validators;
using ProductsMicroService.Application.Mapping;

namespace ProductsMicroService.Application;

public static class ModuleDependencyInjection
{
    public static IServiceCollection AddApplicationDependency(this IServiceCollection services, IConfiguration configuration)
    {
        // Register Mapster
        services.AddMapster();
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        
        // Register Mediatr
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Register Fluent Validation
        services.AddValidatorsFromAssemblyContaining<CreateBrandValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateBrandValidator>();
        
        services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateSubCategoryValidator>();
        
        services.AddValidatorsFromAssemblyContaining<CreateColorValidator>();
        
        return services;
    }
}