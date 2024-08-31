using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupCS
{
    public class BackupService
    {
        public required List<FolderConfig> Folders { get; init; }

        public void MakeCopy()
        {
            foreach (var folder in Folders)
            {
                MakeCopy(folder);
            }
        }

        private void MakeCopy(FolderConfig folder)
        {
            Console.WriteLine($"Create backup for project {folder.Name}");
            var start = DateTime.Now;

            BackupCleaner.RemoveBackupFolder(folder);

            var dstFolderPath = VersionService.BuildOutputDirectoryName(folder);

            var startPointOfFileName = folder.Source!.Length;

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

            // todo remove it!!!
            Console.WriteLine("File list to copy:");
            foreach (var entry in filesToCopy)
            {
                Console.WriteLine($"{entry.Key} -> {entry.Value}");
            }
            var count = 0;
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
                    /*                    Console.Write("\r{0}   ", $"Copied {count} from {filesToCopy.Count} files ({count * 100 / filesToCopy.Count}%)");
                                        Console.Write("\r{0}   ", $"Copied {count} from {filesToCopy.Count} files ({count * 100 / filesToCopy.Count}%)");
                    */
                    /*                    Console.SetCursorPosition(0, Console.CursorTop);
                                        Console.Write($"Copied {count} from {filesToCopy.Count} files ({count * 100 / filesToCopy.Count}%)");
                                        Console.SetCursorPosition(0, Console.CursorTop);
                                        Console.Write($"Copied {count} from {filesToCopy.Count} files ({count * 100 / filesToCopy.Count}%)");
                    */
                    Console.Write("\r{0}", $"Copied {count} from {filesToCopy.Count} files ({count * 100 / filesToCopy.Count}%)");
                    Console.Write("\r{0}", $"Copied {count} from {filesToCopy.Count} files ({count * 100 / filesToCopy.Count}%)");
                    //Console.WriteLine($"Copied {count} from {filesToCopy.Count} files ({count * 100 / filesToCopy.Count}%)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            var now = DateTime.Now;
            var hours = (int)(now - start).TotalHours;
            var minutes = (int)(now - start).TotalMinutes;
            var seconds = (int)(now - start).TotalSeconds;
            Console.WriteLine();
            Console.WriteLine($"Total copied {count} files in {hours}:{minutes}:{seconds} sec");
        }
    }
}
