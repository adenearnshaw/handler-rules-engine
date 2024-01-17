namespace RulesHandlerPoc.RulesEngine;

public class RulesEngineOptions<T> where T : new()
{
    public List<Func<IServiceProvider, RuleHandler<T>>> Rules { get; } = new();
}
