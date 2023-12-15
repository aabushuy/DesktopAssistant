using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ScenarioTool.Shared.Services.ActionHandlers
{
    internal class RunProcessActionHandler : StepActionHandler
    {
        private const string ParamAppPath = "AppPath";

        private readonly ILogger<RunProcessActionHandler> _logger;

        private string appPath = string.Empty;

        public RunProcessActionHandler(ILogger<RunProcessActionHandler> logger)
        {
            _logger = logger;
        }

        protected override Task<bool> InnerValidate()
        {
            appPath = Step?.Params.FirstOrDefault(p => p.Key == ParamAppPath)?.Value
                ?? throw new ArgumentNullException(ParamAppPath);

            return Task.FromResult(!string.IsNullOrEmpty(appPath));
        }

        protected override async Task InnerExecute()
        {
            var p = GetNewProcess(appPath);
            p.StartInfo.Arguments = string.Join(
                " ",
                Step?.Params
                    .Where(p => string.IsNullOrEmpty(p.Key))
                    .Select(p => p.Value)
                    .ToList() ?? new List<string>());

            _logger.LogInformation($"Start process: [{appPath}]");
            _logger.LogInformation($"Process args: [{p.StartInfo.Arguments}]");

            p.Start();

            while (!p.StandardOutput.EndOfStream)
            {
                _logger.LogInformation(p.StandardOutput.ReadLine());
            }
            
            while (!p.StandardError.EndOfStream)
            {
                _logger.LogError(p.StandardError.ReadToEnd());
            }

            await p.WaitForExitAsync();
        }

        private static Process GetNewProcess(string appPath)
        {
            Process p = new();
            p.StartInfo.FileName = appPath;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.UseShellExecute = false;
            p.EnableRaisingEvents = true;

            return p;
        }
    }
}
