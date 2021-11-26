using System;
using System.Threading;
using BackupsExtra.Objects;
using BackupsExtra.Services;

namespace BackupsExtra
{
    internal static class Program
    {
        private static void Main()
        {
            var backup = new BackupJob(OptionsForLogging.ToConsole, false);
            RestorePoint rest1 = backup.LaunchBackup(OptionsForBackup.SplitStorages);
            backup.DeleteJobObjectInQueueBackup("FileB.txt");
            RestorePoint rest2 = backup.LaunchBackup(OptionsForBackup.SingleStorage);

            // Console.WriteLine("-1-");
            // Thread.Sleep(10000);
            // Console.WriteLine("-2-");
            backup.RestoringFilesFromRestorePoint(
                rest2,
                @"C:\Users\HTMLD\Documents\GitHub\ferbator\BackupsExtra\Other Zone Tmp Files",
                OptionsForRestoringFiles.ToDifferentLocation);
        }
    }
}