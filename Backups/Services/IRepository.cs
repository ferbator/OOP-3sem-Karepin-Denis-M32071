using System.Collections.Generic;
using Backups.Objects;

namespace Backups.Services
{
    public interface IRepository
    {
        public RestorePoint BackupInRepoForSingleStorage(List<JobObject> repo, int launchNumber);
        public RestorePoint BackupInRepoForSplitStorages(List<JobObject> repo, int launchNumber);
        public void ClearRepo();
        public int CountStorages();
    }
}