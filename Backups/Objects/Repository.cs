using System;
using System.Collections.Generic;
using System.IO;
using Backups.Services;
using Backups.Tools;

namespace Backups.Objects
{
    public class Repository : IRepository
    {
        private readonly List<Storage> _storages;

        public Repository(string path)
        {
            PathRepo = path ?? throw new BackupException("Incorrect path");
            TimeOfCreate = DateTime.Now;
            _storages = new List<Storage>();
        }

        public string PathRepo { get; }
        public DateTime TimeOfCreate { get; }

        public RestorePoint BackupInRepoForSingleStorage(List<JobObject> repo, int launchNumber)
        {
            var algoFirst = new AlgoSingleStorage(PathRepo);
            var tmp = new List<Storage>(algoFirst.DoAlgorithmic(repo, launchNumber));
            _storages.AddRange(tmp);

            return new RestorePoint(_storages);
        }

        public RestorePoint BackupInRepoForSplitStorages(List<JobObject> repo, int launchNumber)
        {
            var algoSecond = new AlgoSplitStorages(PathRepo);
            var tmp = new List<Storage>(algoSecond.DoAlgorithmic(repo, launchNumber));
            _storages.AddRange(tmp);

            return new RestorePoint(_storages);
        }

        public void ClearRepo()
        {
            var dirInfo = new DirectoryInfo(PathRepo);

            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
        }

        public int CountStorages()
        {
            return _storages.Count;
        }
    }
}