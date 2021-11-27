namespace BackupsExtra.Services
{
#pragma warning disable SA1602
    public enum OptionsForBackup
    {
        SplitStorages,
        SingleStorage,
    }

    public enum OptionsForRestoringFiles
    {
        ToOriginalLocation,
        ToDifferentLocation,
    }

    public enum OptionsForLogging
    {
        ToFile,
        ToConsole,
    }

    public enum TypeOfLogging
    {
        Error,
        Info,
    }

    public enum OptionsForClearingRestorePoint
    {
        ByDate,
        ByCount,
        AllByDateAndCount,
        AllByDateOrCount,
    }
#pragma warning restore SA1602
}