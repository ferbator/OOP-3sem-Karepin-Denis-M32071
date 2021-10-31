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
            string path = Directory.GetCurrentDirectory();
            Console.WriteLine(path);
            var backup = new BackupJob(
                path,
                path);
            backup.DeleteJobObjectInQueueBackup("FileC.txt");
            backup.LaunchBackup(OptionsForBackup.SplitStorages);
        }
    }
}
