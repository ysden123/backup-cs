using BackupCSLib.Service;
using System.Text.Json;

namespace BackupCSTest.Service
{
    public class ConfigTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ParseFolderConfig()
        {
            string json = """
                {
                  "name": "My Documents D",
                  "source": "d:\\My Documents D\\",
                  "destination": "e:\\My Documents D backups\\",
                  "directoriesToSkip": [],
                  "maxBackupDirectories": 2
                }
                """;
            var folderConfig = JsonSerializer.Deserialize<FolderConfig>(json);
            Assert.That(folderConfig, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(folderConfig.Name, Is.EqualTo("My Documents D"));
                Assert.That(folderConfig.Source, Is.EqualTo("d:\\My Documents D\\"));
                Assert.That(folderConfig.Destination, Is.EqualTo("e:\\My Documents D backups\\"));
                Assert.That(folderConfig.DirectoriesToSkip, Is.EqualTo(Array.Empty<string>()));
                Assert.That(folderConfig.MaxBackupDirectories, Is.EqualTo(2));
            });
        }

        [Test]
        public void ParseConfiguration()
        {
            string json = """
                [
                  {
                    "name": "My Documents D",
                    "source": "d:\\My Documents D\\",
                    "destination": "e:\\My Documents D backups\\",
                    "directoriesToSkip": [],
                    "maxBackupDirectories": 2
                  },
                  {
                    "name": "Photo",
                    "source": "d:\\Photo",
                    "destination": "e:\\Photo backups\\",
                    "directoriesToSkip": [
                      "d:\\Photo\\YSPhoto\\Lightroom\\Backups",
                      "d:\\Photo\\YSPhoto\\Lightroom\\YSPhoto-v13 Previews.lrdata",
                      "d:\\Photo\\YSSlides\\Lightroom\\Backups",
                      "d:\\Photo\\YSSlides\\Lightroom\\YSSlides-v13 Previews.lrdata"
                    ],
                    "maxBackupDirectories": 2
                  }
                ]
                """
            ;

            var configuration = JsonSerializer.Deserialize<List<FolderConfig>>(json);
            Assert.That(configuration, Is.Not.Null);
            Assert.That(configuration.ElementAt(1).Name, Is.EqualTo("Photo"));
        }

        [Test]
        public void ParseRealConfiguration()
        {
            var configuration = FolderConfig.ReadConfiguration();
            Assert.That(configuration, Is.Not.Null);
            Assert.That(configuration.ElementAt(1).Name, Is.EqualTo("Photo"));
        }
    }
}