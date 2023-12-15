using ScenarioTool.Shared.Entity;

namespace ScenarioTool.Shared.Services
{
    public interface IScenarioService
    {
        Task<List<Scenario>> GetScenarios(CancellationToken token);
    }
}
