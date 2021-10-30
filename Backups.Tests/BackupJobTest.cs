using Backups.Objects;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupJobTest
    {
        private BackupJob _backup;
        
        [SetUp]
        public void Setup()
        {
            // default path
            _backup = new BackupJob();
        }
        
        [Test]
        public void AddJobObjectsForBackup_FolderOfSetPathHaveThisFile()
        {
            // default folder file: FileA.txt; FileB.txt; FileC.txt;
            if (!_backup.CheckFileInListJobObjects("FileA.txt") && 
                !_backup.CheckFileInListJobObjects("FileB.txt")&&
                !_backup.CheckFileInListJobObjects("FileC.txt"))
                Assert.Fail();
        }
        
        [Test]
        public void CheckBackupWork_TwoRestorePointAndThreeStorage()
        {
            // default folder file: FileA.txt; FileB.txt; FileC.txt;
            _backup.DeleteJobObjectInQueueBackup("FileC.txt");
            // now create two file in queue Backup: FileA.txt; FileB.txt;
            _backup.LaunchBackup(OptionsForBackup.SplitStorages);
            _backup.DeleteJobObjectInQueueBackup("FileB.txt");
            _backup.LaunchBackup(OptionsForBackup.SplitStorages);
            _backup.ClearTmpRepo();
            Assert.IsTrue(_backup.CountRestorePoint() == 2);
            Assert.IsTrue(_backup.CountStoragesInRepo() == 3);
        }
    }
}