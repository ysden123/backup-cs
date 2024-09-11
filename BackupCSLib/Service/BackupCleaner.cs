namespace BackupCSLib.Service
{
    public class BackupCleaner
    {
        public static void RemoveBackupFolder(FolderConfig config, Actions actions)
        {
            if (config == null) return;

            actions.AddTotalLine($"Going to delete previous copy for {config.Name} project");

            if (!Directory.Exists(config.Destination)) return;

            DirectoryInfo di = new(config.Destination);
            var dirs = di.GetDirectories();

            if (dirs == null) return;

            if (dirs.Length < config.MaxBackupDirectories) return;

            var orderredDirs = dirs.OrderBy(dir => dir.CreationTime).ToList();
            var dirForRemove = orderredDirs.First();
            actions.AddTotalLine($"Going to delete {dirForRemove?.FullName}");
            actions.AddTotalLine($"Remove read only attribte {dirForRemove?.FullName}");
            RemoveReadOnlyAttribute(dirForRemove, actions);
            try
            {
                dirForRemove?.Delete(true);
            }
            catch (Exception ex)
            {
                actions.AddTotalLine($"!!!Exception in deleting the directory: {ex.Message}");
            }
        }

        private static void RemoveReadOnlyAttribute(DirectoryInfo? diToRemove, Actions actions)
        {
            if (diToRemove == null) return;
            try
            {
                var directory = new DirectoryInfo(diToRemove!.FullName.ToString()) { Attributes = FileAttributes.Normal };

                foreach (var info in directory.GetFileSystemInfos("*", SearchOption.AllDirectories))
                {
                    if (info.Attributes != FileAttributes.Normal)
                        info.Attributes = FileAttributes.Normal;
                }
            }
            catch (Exception ex)
            {
                actions.AddTotalLine($"!!!Exception in removing read only attribute: {ex.Message}");
            }
        }
    }
}
