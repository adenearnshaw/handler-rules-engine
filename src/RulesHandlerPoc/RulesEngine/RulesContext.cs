namespace RulesHandlerPoc.RulesEngine;

public class RulesContext<T> where T : new()
{
    public T State { get; set; } = new T();
}
