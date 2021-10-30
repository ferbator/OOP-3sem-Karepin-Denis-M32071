using Backups.Objects;

namespace Backups.Services
{
    public interface IBackup
    {
        void DeleteJobObjectInQueueBackup(string name);
        void AddJobObjectInQueueBackup(string name);
        void LaunchBackup(OptionsForBackup option);
        bool CheckFileInListJobObjects(string name);
        void ClearTmpRepo();
        int CountRestorePoint();
        int CountStoragesInRepo();
        void AnalysisOfBackupZone();
    }
}