using Backups.Objects;

namespace Backups.Services
{
    public interface IBackup
    {
        public void DeleteJobObjectInQueueBackup(string name);
        public void AddJobObjectInQueueBackup(string name);
        public void LaunchBackup(OptionsForBackup option);
        public bool CheckFileInListJobObjects(string name);
        public void ClearTmpRepo();
        public int CountRestorePoint();
        public int CountStoragesInRepo();
        public void AnalysisOfBackupZone();
    }
}