using Microsoft.Extensions.Logging;
using ScenarioTool.Shared.Entity;
using System.Diagnostics;

namespace ScenarioTool.Shared.Services.ActionHandlers
{
    internal class CopyFolderActionHandler : StepActionHandler
    {
        private const string ParamDirectoryFrom = "DirectoryFrom";
        private const string ParamDirectoryTo = "DirectoryTo";

        private readonly ILogger<CopyFolderActionHandler> _logger;

        private DirectoryInfo? _diFrom;
        private DirectoryInfo? _diTo;

        public CopyFolderActionHandler(ILogger<CopyFolderActionHandler> logger)
        {
            _logger = logger;
        }

        protected override Task<bool> InnerValidate()
        {
            StepParam[] stepParams = Step?.Params ?? Array.Empty<StepParam>();

            var copyFrom = stepParams.FirstOrDefault(p => p.Key == ParamDirectoryFrom)?.Value
                    ?? throw new ArgumentNullException(ParamDirectoryFrom);

            var copyTo = stepParams.FirstOrDefault(p => p.Key == ParamDirectoryTo)?.Value
                ?? throw new ArgumentNullException(ParamDirectoryTo);

            _diFrom = new DirectoryInfo(copyFrom);
            if (!_diFrom.Exists)
            {
                throw new DirectoryNotFoundException(ParamDirectoryFrom);
            }

            _diTo = new DirectoryInfo(copyTo);
            if (!_diTo.Exists)
            {
                Directory.CreateDirectory(_diTo.FullName);
            }

            return Task.FromResult(true);
        }

        protected override async Task InnerExecute()
        {
            _logger.LogInformation($"Copy from [{_diFrom.FullName}]");

            var stopWatch = Stopwatch.StartNew();

            long copiedBytes = await Task.Run(() => 
            { 
                return CopyFilesRecursively(_diFrom.FullName, _diTo.FullName); 
            });

            _logger.LogInformation($"Copied {GetReadebleSize(copiedBytes)} in {stopWatch.Elapsed.TotalSeconds} seconds.");
        }

        private long CopyFilesRecursively(string sourcePath, string targetPath)
        {
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                _logger.LogInformation($"Create directory [{dirPath}]");

                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));                
            }

            long copiedSize = 0;

            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                var fi = new FileInfo(newPath);
                copiedSize += fi.Length;

                _logger.LogInformation($"Copy file [{fi.Name}] {GetReadebleSize(fi.Length)}");

                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }

            return copiedSize;
        }

        private static string GetReadebleSize(long size, int iteration = 0)
        {
            var newSize = size / 1024;
            if (size > 1024)
            {
                return GetReadebleSize(newSize, ++iteration);
            }

            var postfix = iteration switch
            {
                0 => "bytes",
                1 => "kB",
                2 => "MB",
                3 => "GB",
                4 => "TB",
                _ => "unknown"
            };

            return $"{size} {postfix}";
        }
    }
}
