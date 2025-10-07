using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProductsMicroService.Infrastructure;

public static class ModuleDependencyInjection
{
    public static IServiceCollection AddInfrastructureDependency(this IServiceCollection services, IConfiguration configuration)
    {
        // Register Db Context
        

        
        // Register Repository
        
        
        return services;
    }
}