namespace RulesHandlerPoc.Services;

public class InjectedService
{
    public Task<string> GetCustomString() => Task.FromResult("A");
}
