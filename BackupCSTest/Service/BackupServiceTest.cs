using BackupCSLib.Service;

namespace BackupCSTest.Service
{
    class BackupServiceTest
    {
        private static Actions theActions = new()
        {
            SetProjectName = new Action<string>(n => Console.WriteLine($"Project: {n}")),
            SetActionName = new Action<string>(n => Console.WriteLine($"Action: {n}")),
            InitProgressBar = new Action<int, int>((min, max) => Console.WriteLine($"Init progress bar. min={min}, max={max}")),
            SetProgressBar = new Action<int>((v) => Console.WriteLine($"Set progress bar. value={v}")),
            InitDuraion = new Action(() => Console.WriteLine($"Init duration")),
            SetDuration = new Action<string>(v => Console.WriteLine($"Set duration: {v}")),
            AddTotalLine = new Action<string>(v => Console.WriteLine($"Add total line: {v}"))
        };
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
            BackupService backupService = new() { Folders = [folderConfig], TheActions = theActions };
            backupService.MakeCopy();
        }
    }
}
