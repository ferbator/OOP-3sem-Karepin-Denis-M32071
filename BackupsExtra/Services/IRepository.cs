using System.Collections.Generic;
using BackupsExtra.Objects;

namespace BackupsExtra.Services
{
    public interface IRepository
    {
        void AddStoragesToRepo(List<Storage> storages);
        void ClearRepo();
        int CountStorages();
        string GetPath();
    }
}