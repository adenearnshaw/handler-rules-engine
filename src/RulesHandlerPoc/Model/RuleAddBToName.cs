using RulesHandlerPoc.RulesEngine;

namespace RulesHandlerPoc.Model;
public class RuleAddBToName : RuleHandler<DataModel>
{
    public override Task<RulesResult<DataModel>> Next(RulesContext<DataModel> context)
    {
        context.State.Name += "B";

        return base.Next(context);
    }
}
