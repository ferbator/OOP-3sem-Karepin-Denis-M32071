using System;
using System.Collections.Generic;
using System.Linq;

namespace BackupsExtra.Objects
{
    public class RestorePoint
    {
        private List<Storage> _restorePoint;

        public RestorePoint(List<Storage> storages)
        {
            _restorePoint = storages;
            TimeCreate = DateTime.Now;
        }

        public DateTime TimeCreate { get; }
        public List<Storage> GetStorages => _restorePoint.ToList();
    }
}