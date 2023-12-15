using Microsoft.Extensions.Logging;
using ScenarioTool.Shared.Entity;
using ScenarioTool.Shared.Providers;
using System.Diagnostics;

namespace ScenarioTool.Shared.Services
{
    internal class ScenarioExecutor : IScenarioExecutor
    {
        private readonly IStepActionProvider _stepActionProvider;
        private readonly ILogger<IScenarioExecutor> _logger;

        public ScenarioExecutor(
            IStepActionProvider stepActionProvider,
            ILogger<IScenarioExecutor> logger)
        {
            _stepActionProvider = stepActionProvider;
            _logger = logger;
        }

        public async Task StartScenario(Scenario scenario)
        {
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation($"Start [{scenario.Name}]");

            foreach (var step in scenario.Steps)
            {
                await RunStep(step);
            }

            _logger.LogInformation($"Scenario [{scenario.Name}] completed in {stopwatch.Elapsed.TotalSeconds} sec.");
        }

        private async Task RunStep(ScenarioStep step)
        {
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation($"Step [{step}]");

            var handler = _stepActionProvider.GetHandler(step.ActionType)
                ?? throw new InvalidOperationException($"No such handler {step.ActionType}");

            await handler.Init(step);

            if (await handler.Validate())
            {
                await handler.Execute();

                _logger.LogInformation($"Step [{step}] completed in {stopwatch.Elapsed.TotalSeconds} sec.");
            }
            else 
            {
                _logger.LogError($"[{step}] is not valid. See validation errors:");
                foreach (var error in handler.Errors.Select(e => e.Message))
                {
                    _logger.LogError(error);
                }
            }
        }
    }
}
