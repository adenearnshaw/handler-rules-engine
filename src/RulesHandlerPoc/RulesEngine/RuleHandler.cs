namespace RulesHandlerPoc.RulesEngine;

public abstract class RuleHandler { }

public abstract class RuleHandler<T> : RuleHandler where T : new()
{
    private RuleHandler<T>? _innerHandler;

    public RuleHandler()
    {
    }

    public RuleHandler(RuleHandler<T> innerHandler)
    {
        _innerHandler = innerHandler;
    }

    public RuleHandler<T> SetInner(RuleHandler<T> innerHandler)
    {
        _innerHandler = innerHandler;
        return this;
    }

    public virtual Task<RulesResult<T>> Next(RulesContext<T> context)
    {
        if (_innerHandler is null)
            return Task.FromResult(new RulesResult<T> { State = context.State });

        return _innerHandler!.Next(context);
    }
}
