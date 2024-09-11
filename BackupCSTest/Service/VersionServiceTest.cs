using BackupCSLib.Service;

namespace BackupCSTest.Service
{
    public class VersionServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BuildOutputDirectoryName()
        {
            var folderConfig = new FolderConfig()
            {
                Name = "test",
                Source = "testCleaner",
                Destination = "c:\\work\\testCleaner",
                DirectoriesToSkip = [],
                MaxBackupDirectories = 2
            };
            var dir = VersionService.BuildOutputDirectoryName(folderConfig);
            Assert.That(dir, Is.Not.Null);
            Console.WriteLine(dir);

            folderConfig = new FolderConfig()
            {
                Name = "test",
                Source = "testCleaner",
                Destination = "c:\\work\\testCleaner\\",
                DirectoriesToSkip = [],
                MaxBackupDirectories = 2
            };
            Assert.That(dir, Is.Not.Null);
            Console.WriteLine(dir);
        }
    }
}
