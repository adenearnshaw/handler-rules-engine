using Microsoft.Extensions.DependencyInjection;

namespace RulesHandlerPoc.RulesEngine;

/// <summary>
/// Builder used to configure the rules engine
/// </summary>
/// <typeparam name="T">RulesContext State type</typeparam>
public class RulesEngineBuilder<T> where T : new()
{
    public string Name { get; }
    public virtual IServiceCollection Services { get; }

    public RulesEngineBuilder(IServiceCollection services)
    {
        Name = typeof(T).Name;
        Services = services;
    }

    public RulesEngineBuilder<T> AddRule<R>() where R : RuleHandler<T>
    {
        Services.AddTransient<R>();
        Services.Configure<RulesEngineOptions<T>>(Name, options =>
        {
            options.Rules.Add((sp) => sp.GetRequiredService<R>());
        });

        return this;
    }
}
