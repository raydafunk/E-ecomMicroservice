using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastruture;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connnectionString = configuration.GetConnectionString("Database");

        //add services to the container 
        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connnectionString));


        return services;
    }
}
