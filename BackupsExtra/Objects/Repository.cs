using System;
using System.Collections.Generic;
using System.IO;
using BackupsExtra.Services;
using BackupsExtra.Tools;

namespace BackupsExtra.Objects
{
    public class Repository : IRepository
    {
        private readonly List<Storage> _storages;

        public Repository(string path)
        {
            PathRepo = path ?? throw new BackupsExtraException("Incorrect path");
            TimeOfCreate = DateTime.Now;
            _storages = new List<Storage>();
        }

        public string PathRepo { get; }
        public DateTime TimeOfCreate { get; }

        public void AddStoragesToRepo(List<Storage> storages)
        {
            _storages.AddRange(storages);
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