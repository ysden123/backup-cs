namespace BackupCS
{
    public static class VersionService
    {
        private static string BuildOutputBackupDirectoryName()
        {
            var cd = DateTime.Now;
            return cd.ToString("yyyy_MM_dd_HH_mm_ss");
        }

        public static string BuildOutputDirectoryName(FolderConfig folderConfig)
        {
            return Path.Combine(folderConfig.Destination!, BuildOutputBackupDirectoryName());
/*            if (folderConfig.Destination!.EndsWith('\\'))
            {
                return folderConfig.Destination + BuildOutputBackupDirectoryName();
            }
            else
            {
                return folderConfig.Destination + "\\" + BuildOutputBackupDirectoryName();
            }
*/        }
    }
}
