using Microsoft.Extensions.Options;

namespace RulesHandlerPoc.RulesEngine;

public class RulesEngine<T> where T : new()
{
    private readonly List<Func<IServiceProvider, RuleHandler<T>>> rules;
    private readonly IServiceProvider serviceProvider;

    public RulesEngine(IServiceProvider serviceProvider,
                       IOptionsMonitor<RulesEngineOptions<T>> optionsMonitor)
    {
        rules = optionsMonitor.Get(typeof(T).Name).Rules;
        this.serviceProvider = serviceProvider;
    }

    public async Task<RulesResult<T>> Run(RulesContext<T> context)
    {
        RuleHandler<T>? primaryRule = new PrimaryRule<T>();

        if (rules is null || rules.Count == 0)
            return await primaryRule.Next(context);

        for (int i = rules.Count - 1; i >= 0; i--)
        {
            var rule = rules[i](serviceProvider);
            _ = rule.SetInner(i == rules.Count - 1 ? null : rules[i + 1](serviceProvider));
            primaryRule = rule;
        }

        return await primaryRule.Next(context);
    }
}
