using Microsoft.Extensions.DependencyInjection;
using SoccerInfo.Domain.Lookups.GeneralPosition;

namespace SoccerInfo.Domain;
public static class ServiceCollectionExtensions
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<GeneralPositionService>();
    }
}
