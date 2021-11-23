using System.IO;
using Backups.Objects;
using Backups.Services;

namespace Backups
{
    internal static class Program
    {
        private static void Main()
        {
            string path = Directory.GetCurrentDirectory();
            var backup = new BackupJob(Path.Combine(path, "Zone Backup"), path);

            backup.DeleteJobObjectInQueueBackup("FileC.txt");
            backup.LaunchBackup(OptionsForBackup.SingleStorage);
        }
    }
}