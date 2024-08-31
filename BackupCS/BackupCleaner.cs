namespace BackupCS
{
    public class BackupCleaner
    {
        public static void RemoveBackupFolder(FolderConfig config)
        {
            if (config == null) return;

            Console.WriteLine($"Going to delete previous copy for {config.Name} project");

            if (!Directory.Exists(config.Destination)) return;

            DirectoryInfo di = new(config.Destination);
            var dirs = di.GetDirectories();

            if (dirs == null) return;

            if (dirs.Length < config.MaxBackupDirectories) return;

            var orderredDirs = dirs.OrderBy(dir => dir.CreationTime).ToList();
            var dirForRemove = orderredDirs.First();
            Console.WriteLine($"Going to delete {dirForRemove?.FullName}");

            dirForRemove?.Delete(true);
        }
    }
}
