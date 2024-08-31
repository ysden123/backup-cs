using BackupCS;
namespace BackupCSTest
{
    public class BackupCleanerTest
    {
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

            BackupCleaner.RemoveBackupFolder(folderConfig);
            Assert.Pass();
        }
    }
}
