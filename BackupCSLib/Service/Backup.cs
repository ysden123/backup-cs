using Serilog;

namespace BackupCSLib.Service
{
    public class Backup
    {
        private readonly ILogger _logger;
        public required Actions TheActionsactions { get; init; }

        public Backup()
        {
            _logger = Log.ForContext<Backup>();
        }

        private List<FolderConfig> ReadConfiguration()
        {
            var folders = FolderConfig.ReadConfiguration();
            if (folders == null)
            {
                var errorMessage = "Can't read configuration file or file is empty.";
                _logger.Error(errorMessage);
                TheActionsactions.AddTotalLine(errorMessage);
                return [];
            }
            else
            {
                return folders;
            }
        }

        public void Run()
        {
            Thread thread = new(new ThreadStart(() =>
            {
                BackupService backupService = new() { Folders = ReadConfiguration(), TheActions = this.TheActionsactions };
                backupService.MakeCopy();
            }));
            thread.Start();
        }
    }
}
