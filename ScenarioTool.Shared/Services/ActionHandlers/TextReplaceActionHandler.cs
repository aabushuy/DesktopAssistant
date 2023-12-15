using ScenarioTool.Shared.Entity;

namespace ScenarioTool.Shared.Services.ActionHandlers
{
    internal class TextReplaceActionHandler : StepActionHandler
    {
        private const string ParamFilePath = "FilePath";
        private const string ParamFrom = "From";
        private const string ParamTo = "To";

        private FileInfo? _fileInfo;
        private string? _from;
        private string? _to;

        protected override Task<bool> InnerValidate()
        {
            StepParam[] stepParams = Step?.Params ?? Array.Empty<StepParam>();

            var targetFile = stepParams.FirstOrDefault(p => p.Key == ParamFilePath)?.Value
                    ?? throw new ArgumentNullException(ParamFilePath);

            _fileInfo = new FileInfo(targetFile);

            if (!_fileInfo.Exists)
            {
                throw new FileNotFoundException(targetFile);
            }

            _from = stepParams.FirstOrDefault(p => p.Key == ParamFrom)?.Value
                ?? throw new ArgumentNullException(ParamFrom);

            _to = stepParams.FirstOrDefault(p => p.Key == ParamTo)?.Value
                ?? throw new ArgumentNullException(ParamTo);

            if (string.IsNullOrWhiteSpace(_from))
            {
                throw new InvalidOperationException($"Empty argument {nameof(ParamFrom)}");
            }

            if (string.IsNullOrWhiteSpace(_to))
            {
                throw new InvalidOperationException($"Empty argument {nameof(ParamTo)}");
            }

            return Task.FromResult(true);
        }

        protected override async Task InnerExecute()
        {
            string text = await File.ReadAllTextAsync(_fileInfo.FullName);

            text = text.Replace(_from, _to);

            await File.WriteAllTextAsync(_fileInfo.FullName, text);
        }
    }
}
