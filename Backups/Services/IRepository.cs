using System.Collections.Generic;
using Backups.Objects;

namespace Backups.Services
{
    public interface IRepository
    {
        void AddStoragesToRepo(List<Storage> storages);
        void ClearRepo();
        int CountStorages();
    }
}