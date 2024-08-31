using BackupCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupCSTest
{
    class BackupServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MakeyCopyTest1()
        {
/*            var folderConfig = new FolderConfig()
            {
                Name = "test",
                Source = "c:\\work\\src\\",
                Destination = "c:\\work\\testCleaner",
                DirectoriesToSkip = [],
                MaxBackupDirectories = 2
            };

            BackupService backupService = new () { Folders = [folderConfig] };
            backupService.MakeCopy();
*/
            var folderConfig = new FolderConfig()
            {
                Name = "test",
                Source = "c:\\work\\src\\",
                Destination = "c:\\work\\testCleaner",
                DirectoriesToSkip = ["c:\\work\\src\\sd1", "c:\\work\\src\\sd3"],
                MaxBackupDirectories = 2
            };
            BackupService backupService = new() { Folders = [folderConfig] };
            backupService.MakeCopy();
        }
    }
}
