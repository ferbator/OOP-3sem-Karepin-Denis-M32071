﻿using Backups.Objects;
using Backups.Services;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            var backup = new BackupJob(
                @"C:\Users\HTMLD\Documents\GitHub\ferbator\Backups\Zone Backup",
                @"C:\Users\HTMLD\Documents\GitHub\ferbator\Backups\Zone Tmp File");
            backup.DeleteJobObjectInQueueBackup("FileC.txt");
            backup.LaunchBackup(OptionsForBackup.SplitStorages);
        }
    }
}
