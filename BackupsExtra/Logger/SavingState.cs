using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BackupsExtra.Objects;

namespace BackupsExtra.Logger
{
    public class SavingState
    {
        private BinaryFormatter _formatter;
        private BackupJob _serialization;
        public SavingState(BackupJob backupJob)
        {
            _formatter = new BinaryFormatter();
            _serialization = backupJob;
        }

        public void GetStream()
        {
            var fs = new FileStream("./BackupJob.dat", FileMode.OpenOrCreate);
            _formatter.Serialize(fs, _serialization);
        }

        public void Deserialize()
        {
            var fs = new FileStream("./BackupJob.dat", FileMode.OpenOrCreate);
            _serialization = (BackupJob)_formatter.Deserialize(fs);
        }
    }
}