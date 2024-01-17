using Microsoft.Extensions.DependencyInjection;
using RulesHandlerPoc.Model;
using RulesHandlerPoc.RulesEngine;
using RulesHandlerPoc.Services;

// Configuration
var services = new ServiceCollection();

services.AddTransient<InjectedService>();

services.AddRulesEngine<DataModel>()
        .AddRule<RuleAddAToName>()
        .AddRule<RuleAddBToName>();


var serviceProvider = services.BuildServiceProvider();


// Usage
var engine = serviceProvider.GetRequiredService<RulesEngine<DataModel>>();

var dataModel = new DataModel
{
    Name = "Name"
};

var result = await engine.Run(new RulesContext<DataModel> { State = dataModel });

// Output
Console.WriteLine(result.State?.Name ?? "Unknown");
Console.ReadLine();
