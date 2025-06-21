using Microsoft.Extensions.DependencyInjection;
using SoccerInfo.Domain.Models.GeneralPosition;

namespace SoccerInfo.Domain;
public static class ServiceCollectionExtensions
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<GeneralPositionService>();
    }
}
