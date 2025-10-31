using Serilog;
using System.IO;

namespace BackupCSApp
{
    internal static class LogBuilder
    {
        private static bool _isInitialized = false;

        public static void Initialize(string assemblyName, string logFilePrefix)
        {
            if (!_isInitialized)
            {
                var folder = YSCommon.Utils.GetAssemblyFolderInLocalData(assemblyName);
                string fileName = Path.Combine(folder, "logs", $"{logFilePrefix}.log");
#if DEBUG
                Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Debug()
                   .Enrich.WithThreadId()
                   .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} [{ThreadId}] {Message:lj}{NewLine}{Exception}")
                   .WriteTo.File(fileName,
                   rollingInterval: RollingInterval.Month,
                   outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {SourceContext} [{ThreadId}] {Message:lj}{NewLine}{Exception}")
               .CreateLogger();
#else
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Information()
               .Enrich.WithThreadId()
               .WriteTo.File(fileName,
               rollingInterval: RollingInterval.Month,
               outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {SourceContext} [{ThreadId}] {Message:lj}{NewLine}{Exception}")
           .CreateLogger();
#endif
                _isInitialized = true;
            }
        }
    }
}
