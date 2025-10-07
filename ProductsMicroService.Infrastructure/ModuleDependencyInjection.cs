using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsMicroService.Infrastructure.Context;

namespace ProductsMicroService.Infrastructure;

public static class ModuleDependencyInjection
{
    public static IServiceCollection AddInfrastructureDependency(this IServiceCollection services, IConfiguration configuration)
    {
        // Register Db Context
        var connectionStringTemplate = configuration.GetConnectionString("MySqlConnection")!;

        var connectionString = connectionStringTemplate
            .Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST") ?? "localhost")
            .Replace("$MYSQL_PORT", Environment.GetEnvironmentVariable("MYSQL_PORT") ?? "3306")
            .Replace("$MYSQL_DATABASE", Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? "EuphoriaProducts")
            .Replace("$MYSQL_USER", Environment.GetEnvironmentVariable("MYSQL_USER") ?? "root")
            .Replace("$MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? "Admin1234");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySQL(connectionString);
        });
        
        
        // Register Repository
        
        
        return services;
    }
}