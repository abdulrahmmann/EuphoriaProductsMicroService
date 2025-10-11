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

        // Read environment variables with fallback values
        var host = Environment.GetEnvironmentVariable("MYSQL_HOST") ?? "localhost";
        var port = Environment.GetEnvironmentVariable("MYSQL_PORT") ?? "3306";
        var databaseName = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? "EuphoriaProducts";
        var userName = Environment.GetEnvironmentVariable("MYSQL_USER") ?? "root";
        var password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? "Admin1234";
        
        var connectionString = connectionStringTemplate
            .Replace("$MYSQL_HOST", host)
            .Replace("$MYSQL_PORT", port)
            .Replace("$MYSQL_DATABASE", databaseName)
            .Replace("$MYSQL_USER", userName)
            .Replace("$MYSQL_PASSWORD", password);

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySQL(connectionString);
        });
        
        
        // Register Repository
        
        
        return services;
    }
}