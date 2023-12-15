using Microsoft.Extensions.Logging.Console;
using ScenarioTool.ConsoleApp;
using ScenarioTool.ConsoleApp.Log;
using ScenarioTool.Shared;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services
            .AddHostedService<Worker>()
            .AddScenarioCore(hostContext.Configuration)
            .AddLogging(builder =>
            {
                builder
                    .AddConsole(options => options.FormatterName = SimpleConsoleFormatter.SimpleConsole)
                    .AddConsoleFormatter<SimpleConsoleFormatter, ConsoleFormatterOptions>();
            });
    })
    .Build();
host.Run();
