# Handler-style Rules Engine

A sample of a rules engine that follows a similar pattern to the C# HttpMessageHandler. 

By following the same patterns as the `IHttpClientBuilder.AddHttpMessageHandler<>`, you can configure a Rules Engine which allows chaining of rules in a specified order without having to explicitly set that order.

By using the builder pattern and registering each handler with in the `IServiceCollection`, each handler can support dependency injection of additional services.

## Using

### Writing a rule

First create a data object that will hold any state you wish to pass between Rules.

Once in place, create a rule by inheriting from `RuleHandler<T>` where T is the Type of your data object.

Then override the `Next()` method with the logic you wish to include in your rule. 

To proceed to the next rule, invoke `base.Next()`, or to short circuit simply return a new instance of RulesResult.

```csharp
public class RuleAddAToName(InjectedService injectedService) : RuleHandler<DataModel>
{
    public override async Task<RulesResult<DataModel>> Next(RulesContext<DataModel> context)
    {
        context.State.Name += await injectedService.GetCustomString();

        return await base.Next(context);
    }
}
```

> As with HttpHandlers, the result bubbles back up the rule chain from the last Handler back to the first, allowing you to perform logic both ahead of the executiion of the next rule, and after too.

### Configure

Call `services.AddRulesEngine<>` then add your rules in the order of execution

``` csharp
var services = new ServiceCollection();

services.AddTransient<InjectedService>();

services.AddRulesEngine<DataModel>()
        .AddRule<RuleAddAToName>()
        .AddRule<RuleAddBToName>();
```

### Execution

Inject an instance of the RulesEngine into your code, create an instance of your dataModel and then call `RulesEngine.Run()`:

```csharp
var dataModel = new DataModel
{
    Name = "Name"
};

var result = await engine.Run(new RulesContext<DataModel> { State = dataModel });
```

## Disclaimer

This was an experiment into the art of the possible and is purely a proof of concept and is no way ready for production use out of the box. 
