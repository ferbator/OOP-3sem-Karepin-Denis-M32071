using System;
using System.Threading;
using BackupsExtra.Logger;
using BackupsExtra.Objects;
using BackupsExtra.Services;

namespace BackupsExtra
{
    internal static class Program
    {
        private static void Main()
        {
            var backup = new BackupJob(
                OptionsForLogging.ToConsole,
                false,
                OptionsForClearingRestorePoint.ByCount,
                2,
                new DateTime(2021, 11, 26, 17, 49, 10));
            var saveProgram = new SavingState(backup);

            RestorePoint rest1 = backup.LaunchBackup(OptionsForBackup.SplitStorages, true);
            backup.DeleteJobObjectInQueueBackup("FileB.txt");
            RestorePoint rest2 = backup.LaunchBackup(OptionsForBackup.SingleStorage, true);
            backup.AddJobObjectInQueueBackup("FileB.txt");
            RestorePoint rest3 = backup.LaunchBackup(OptionsForBackup.SplitStorages, false);

            backup.RestoringFilesFromRestorePoint(
                rest2,
                @"C:\Users\HTMLD\Documents\GitHub\ferbator\BackupsExtra\Other Zone Tmp Files",
                OptionsForRestoringFiles.ToDifferentLocation);

            saveProgram.GetStream();
        }
    }
}