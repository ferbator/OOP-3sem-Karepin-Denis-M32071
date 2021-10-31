using System;
using System.Collections.Generic;

namespace Backups.Objects
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
    }
}