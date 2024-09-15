using System.IO;

namespace BackupCSLib.Service
{
    public class BackupService
    {
        public required List<FolderConfig> Folders { get; init; }
        public required Actions TheActions { get; init; }

        public void MakeCopy()
        {
            foreach (var folder in Folders)
            {
                MakeCopy(folder);
            }

            TheActions.AddTotalLine("\nAll done");
            TheActions.InitProgressBar(0, 100);
            TheActions.SetProgressBar(0);
            TheActions.SetProjectName("");
            TheActions.SetActionName("All done");
            TheActions.SetDuration("");
        }

        private void MakeCopy(FolderConfig folder)
        {
            try
            {
                //Console.WriteLine($"Create backup for project {folder.Name}");
                TheActions.AddTotalLine("\n");
                TheActions.SetProjectName(folder.Name!);
                TheActions.AddTotalLine($"Processing {folder.Name!}");
                TheActions.InitDuraion();
                TheActions.InitProgressBar(0, 100);
                TheActions.SetProgressBar(0);
                var start = DateTime.Now;

                TheActions.SetActionName("Remove previous back folder");
                BackupCleaner.RemoveBackupFolder(folder, TheActions);

                var dstFolderPath = VersionService.BuildOutputDirectoryName(folder);

                var startPointOfFileName = folder.Source!.Length;

                TheActions.SetActionName("Getting file list...");
                TheActions.AddTotalLine("Getting file list...");
                //Build source file list
                var filesToCopy = new Dictionary<string, string>(); // Key - src file name, value - destination file name
                foreach (var file in Directory.GetFiles(folder.Source!, "*", SearchOption.AllDirectories))
                {
                    var toSkip = false;
                    foreach (var skipFolder in folder.DirectoriesToSkip!)
                    {
                        if (file.StartsWith(skipFolder))
                        {
                            toSkip = true;
                            break;
                        }
                    }

                    if (!toSkip) filesToCopy.Add(file, Path.Combine(dstFolderPath, file[startPointOfFileName..]));
                }

                TheActions.SetActionName($"Backup to {dstFolderPath}");
                TheActions.AddTotalLine($"Backup {filesToCopy.Count} files to {dstFolderPath}");
                TheActions.InitProgressBar(0, filesToCopy.Count);
                TheActions.SetProgressBar(0);
                // todo remove it!!!
                /*Console.WriteLine("File list to copy:");
                foreach (var entry in filesToCopy)
                {
                    Console.WriteLine($"{entry.Key} -> {entry.Value}");
                }*/
                var count = 0;
                var outputStep = 100;
                var outputCount = 0;
                // Copy files
                // todo copy files
                foreach (var entry in filesToCopy)
                {
                    try
                    {
                        var parentDir = Directory.GetParent(entry.Value);
                        if (!parentDir!.Exists) parentDir.Create();
                        File.Copy(entry.Key, entry.Value, true);
                        count++;
                        if (++outputCount >= outputStep)
                        {
                            TheActions.SetProgressBar(count);
                            TheActions.SetDuration(BuildDurationText(start));
                            outputCount = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        TheActions.AddTotalLine($"!!!ERROR: {ex.Message}");
                    }
                }
                TheActions.SetProgressBar(count);
                TheActions.AddTotalLine($"Total copied {count} files in {BuildDurationText(start)}");
            }
            catch (Exception e)
            {
                TheActions.AddTotalLine($"ERROR: {e.Message}");
            }
        }

        private static string BuildDurationText(DateTime start)
        {
            var now = DateTime.Now;
            var dif = now - start;
            var hours = dif.Hours;
            var minutes = dif.Minutes;
            var seconds = dif.Seconds;
            return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }
    }
}
