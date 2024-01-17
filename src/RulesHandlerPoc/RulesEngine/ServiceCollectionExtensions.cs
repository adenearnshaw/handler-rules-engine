using Microsoft.Extensions.DependencyInjection;

namespace RulesHandlerPoc.RulesEngine;

public static class ServiceCollectionExtensions
{
    public static RulesEngineBuilder<T> AddRulesEngine<T>(this IServiceCollection services) where T : new()
    {
        services.AddTransient<RulesEngine<T>>();
        services.AddTransient<PrimaryRule<T>>();
        return new RulesEngineBuilder<T>(services);
    }
}
