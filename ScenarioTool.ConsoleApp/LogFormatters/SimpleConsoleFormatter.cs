using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;

namespace ScenarioTool.ConsoleApp.LogFormatters
{
    public sealed class SimpleConsoleFormatter : ConsoleFormatter, IDisposable
    {
        public static string SimpleConsole = "SimpleConsole";

        public SimpleConsoleFormatter() : base(SimpleConsole) { }

        public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
        {
            string? message = logEntry.Formatter?.Invoke(logEntry.State, logEntry.Exception);
            if (message is null)
            {
                return;
            }

            SetConsoleColor(logEntry.LogLevel);

            if (logEntry.LogLevel != LogLevel.Information)
            {
                WriteLogLevel(logEntry.LogLevel, textWriter);
                WriteTimeStamp(textWriter);
            }

            textWriter.WriteLine(message);
        }

        private static void WriteTimeStamp(TextWriter textWriter)
            => textWriter.Write($"{DateTime.Now:hh:mm:ss}|");


        private static void WriteLogLevel(LogLevel logLevel, TextWriter textWriter)
        {
            var level = logLevel.ToString().ToUpper()[..4];

            textWriter.Write($"{level}|");
        }

        private void SetConsoleColor(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Warning:
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case LogLevel.Error:
                case LogLevel.Critical:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.Trace:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case LogLevel.Debug:
                case LogLevel.Information:
                case LogLevel.None:                
                default:
                    Console.ResetColor();
                    break;
            }
        }

        public void Dispose() { }
    }
}
