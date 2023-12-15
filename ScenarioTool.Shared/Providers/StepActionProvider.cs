using ScenarioTool.Shared.Entity;
using ScenarioTool.Shared.Services;
using static ScenarioTool.Shared.IoC;

namespace ScenarioTool.Shared.Providers
{
    internal class StepActionProvider : IStepActionProvider
    {
        private readonly StepActionServiceResolver _stepActionServiceResolver;

        public StepActionProvider(StepActionServiceResolver stepActionServiceResolver)
        {
            _stepActionServiceResolver = stepActionServiceResolver;
        }

        public IStepActionHandler GetHandler(ScenarioActionType actionType) => _stepActionServiceResolver(actionType);
    }
}
