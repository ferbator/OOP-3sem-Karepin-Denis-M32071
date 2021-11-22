using System;
using System.IO;
using Backups.Objects;
using Backups.Services;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            // string path = Directory.GetCurrentDirectory();
            var backup = new BackupJob();

            // backup.DeleteJobObjectInQueueBackup("FileC.txt");
            backup.LaunchBackup(OptionsForBackup.SingleStorage);
        }
    }
}