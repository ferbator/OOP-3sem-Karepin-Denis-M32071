using System;
using System.Collections.Generic;
using System.Linq;

namespace BackupsExtra.Objects
{
    [Serializable]
    public class RestorePoint
    {
        private List<Storage> _restorePoint;

        public RestorePoint()
        {
            _restorePoint = new List<Storage>();
            TimeCreate = null;
        }

        public RestorePoint(List<Storage> storages)
        {
            _restorePoint = storages;
            TimeCreate = DateTime.Now;
        }

        public DateTime? TimeCreate { get; }
        public List<Storage> GetStorages => _restorePoint.ToList();
    }
}