using System;
using System.IO;
using BackupsExtra.Objects;
using BackupsExtra.Services;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    [TestFixture]
    public class BackupJobTest
    {
        
            private BackupJob _backup;
            private string _path;
            [SetUp]
            public void Setup()
            {
                _path = Directory.GetCurrentDirectory();
                // default path
                _backup = new BackupJob(Path.Combine(_path, "Zone Backup"), _path,  OptionsForLogging.ToConsole,
                    false,
                    OptionsForClearingRestorePoint.ByCount,
                    2,
                    new DateTime(2021, 11, 26, 17, 49, 10));
            }
        
            [Test]
            public void AddJobObjectsForBackup_FolderOfSetPathHaveThisFile()
            { // default folder file: FileA.txt; FileB.txt;
                Assert.IsTrue(_backup.CheckFileInListJobObjects("FileA.txt"));
                Assert.IsTrue(_backup.CheckFileInListJobObjects("FileB.txt"));
            }
        
            [Test]
            public void CheckBackupWork_TwoRestorePointAndThreeStorage()
            {
                // default folder file: FileA.txt; FileB.txt;
                _backup.LaunchBackup(OptionsForBackup.SplitStorages, true);
                _backup.DeleteJobObjectInQueueBackup("FileС.txt");
                _backup.LaunchBackup(OptionsForBackup.SingleStorage, true);
                Assert.IsTrue(_backup.CountRestorePoint() == 2);
                Assert.IsTrue(_backup.CountStoragesInRepo() == 4);
            }
    }
}