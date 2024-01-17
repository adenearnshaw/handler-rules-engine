using RulesHandlerPoc.RulesEngine;
using RulesHandlerPoc.Services;

namespace RulesHandlerPoc.Model;
public class RuleAddAToName(InjectedService injectedService) : RuleHandler<DataModel>
{
    public override async Task<RulesResult<DataModel>> Next(RulesContext<DataModel> context)
    {
        context.State.Name += await injectedService.GetCustomString();

        return await base.Next(context);
    }
}
