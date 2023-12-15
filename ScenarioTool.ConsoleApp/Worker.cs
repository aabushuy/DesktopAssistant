using ScenarioTool.Shared.Services;
using System.Globalization;
using System.Text;

namespace ScenarioTool.ConsoleApp
{
    public class Worker : BackgroundService
    {
        private readonly IScenarioService _scenarioService;
        private readonly IScenarioExecutor _scenarioExecutor;
        private readonly ILogger<Worker> _logger;

        public Worker(
            IScenarioService scenarioService,
            IScenarioExecutor scenarioExecutor,
            ILogger<Worker> logger)
        {
            _scenarioService = scenarioService;
            _scenarioExecutor = scenarioExecutor;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);

                try
                {
                    await RunLoop(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }

        private async Task RunLoop(CancellationToken cancellationToken)
        {
            Console.WriteLine();
            
            var scenarios = await _scenarioService.GetScenarios(cancellationToken);

            Console.WriteLine("Choose scenario:");
            for (int i = 0; i < scenarios.Count; i++)
            {
                Console.WriteLine($"{i} - {scenarios[i].Name}");
            }
            
            string command = Console.ReadLine() ?? string.Empty;

            if (!string.IsNullOrEmpty(command)
                && int.TryParse(command, out int commandIndex)
                && commandIndex > -1
                && commandIndex < scenarios.Count)
            {
                var scenario = scenarios[commandIndex];

                try
                {
                    await _scenarioExecutor.StartScenario(scenario);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    _logger.LogCritical($"[{scenario}] has been terminated");
                }
            }
            else
            {
                _logger.LogWarning($"Wrong scenario number");
            }
        }
    }
}