using Microsoft.Extensions.DependencyInjection;

namespace YES;

public static class Services
{
    public static IServiceCollection AddYESClients(this IServiceCollection services) => services
        .AddScoped<IHttpClient, HttpClient>()
        .AddTransient<IYESClient, YESClient>();
}
