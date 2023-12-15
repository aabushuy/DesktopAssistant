using ScenarioTool.Shared.Entity;

namespace ScenarioTool.Shared.Services
{
    public interface IScenarioExecutor
    {
        Task StartScenario(Scenario scenario);
    }
}
