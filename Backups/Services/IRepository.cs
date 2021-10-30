using System.Collections.Generic;
using Backups.Objects;

namespace Backups.Services
{
    public interface IRepository
    {
        RestorePoint BackupInRepoForSingleStorage(List<JobObject> repo, int launchNumber);
        RestorePoint BackupInRepoForSplitStorages(List<JobObject> repo, int launchNumber);
        void ClearRepo();
        int CountStorages();
    }
}