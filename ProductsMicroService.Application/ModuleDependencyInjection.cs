using System.Reflection;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProductsMicroService.Application;

public static class ModuleDependencyInjection
{
    public static IServiceCollection AddApplicationDependency(this IServiceCollection services, IConfiguration configuration)
    {
        // Register Mapster
        services.AddMapster();
        var config = TypeAdapterConfig.GlobalSettings;
        // config.Scan(typeof(UserMappingConfig).Assembly);
        
        // Register Mediatr
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Register Fluent Validation
        
        
        return services;
    }
}