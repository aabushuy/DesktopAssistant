using ScenarioTool.Shared.Entity;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;

namespace ScenarioTool.Shared.Services
{
    internal class ScenarioService : IScenarioService
    {
        private readonly CoreSettings _coreSettings;
        private readonly ILogger<ScenarioService> _logger;
        private readonly XmlSerializer _scenarioFormatter = new(typeof(Scenario));

        public ScenarioService(CoreSettings coreSettings, ILogger<ScenarioService> logger)
        {
            _coreSettings = coreSettings;
            _logger = logger;
        }

        public Task<List<Scenario>> GetScenarios(CancellationToken token)
        {
            var result = new List<Scenario>();

            var di = new DirectoryInfo(_coreSettings.ScenarioFolder);

            _logger.LogInformation("Load scenarios from:{0}", di.FullName);

            foreach (var file in di.GetFiles())
            {
                using FileStream fs = new(file.FullName, FileMode.OpenOrCreate);

                if (_scenarioFormatter.Deserialize(fs) is Scenario scenario)
                {
                    result.Add(scenario);
                }
            }

            return Task.FromResult(result);
        }
    }
}
