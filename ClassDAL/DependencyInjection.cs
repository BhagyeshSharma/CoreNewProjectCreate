using Microsoft.Extensions.DependencyInjection;

namespace ClassDAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDALService(this IServiceCollection services)
    {
        services.AddScoped<dbRepository, IdbRepository>();
        return services;
    }
}
