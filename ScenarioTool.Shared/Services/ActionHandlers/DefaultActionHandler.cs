namespace ScenarioTool.Shared.Services.ActionHandlers
{
    internal class DefaultActionHandler : StepActionHandler
    {
        protected override Task<bool> InnerValidate() => Task.FromResult(false);
    }
}
