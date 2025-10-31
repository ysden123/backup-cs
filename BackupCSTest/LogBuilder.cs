using Serilog;

namespace BackupCSTest
{
    internal class LogBuilder
    {
        private static bool _isInitialized = false;

        public static void Initialize()
        {
            if (!_isInitialized)
            {
                Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Debug()
                   .Enrich.WithThreadId()
                   .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} [{ThreadId}] {Message:lj}{NewLine}{Exception}")
               .CreateLogger();
                _isInitialized = true;
            }
        }
    }
}
