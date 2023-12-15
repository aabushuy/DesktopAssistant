using ScenarioTool.Shared.Entity;
using ScenarioTool.Shared.Services;

namespace ScenarioTool.Shared.Providers
{
    public interface IStepActionProvider
    {
        IStepActionHandler GetHandler(ScenarioActionType actionType);
    }
}
