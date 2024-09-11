namespace BackupCSLib.Service
{
    public class Backup
    {
        public required Actions TheActionsactions { get; init; }
        private List<FolderConfig> ReadConfiguration()
        {
            var folders = FolderConfig.ReadConfiguration();
            if (folders == null)
            {
                TheActionsactions.AddTotalLine("Can't read configuration");
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
