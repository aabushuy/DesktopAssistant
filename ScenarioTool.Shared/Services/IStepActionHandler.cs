using ScenarioTool.Shared.Entity;

namespace ScenarioTool.Shared.Services
{
    public interface IStepActionHandler
    {
        IReadOnlyCollection<Exception> Errors { get; }

        Task Init(ScenarioStep step);

        Task<bool> Validate();

        Task Execute();
    }
}
