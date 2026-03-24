using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Intercom.HttpClients.Registrars;
using Soenneker.Intercom.OpenApiClientUtil.Abstract;

namespace Soenneker.Intercom.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class IntercomOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IntercomOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddIntercomOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddIntercomOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IIntercomOpenApiClientUtil, IntercomOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IntercomOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddIntercomOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddIntercomOpenApiHttpClientAsSingleton()
                .TryAddScoped<IIntercomOpenApiClientUtil, IntercomOpenApiClientUtil>();

        return services;
    }
}
