using BackupCSLib.Service;
namespace BackupCSTest.Service
{
    public class BackupCleanerTest
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
        public void RemoveBackupFolder()
        {
            var folderConfig = new FolderConfig()
            {
                Name = "test",
                Source = "testCleaner",
                Destination = "c:\\work\\testCleaner",
                DirectoriesToSkip = [],
                MaxBackupDirectories = 2
            };

            BackupCleaner.RemoveBackupFolder(folderConfig, theActions);
            Assert.Pass();
        }
    }
}
