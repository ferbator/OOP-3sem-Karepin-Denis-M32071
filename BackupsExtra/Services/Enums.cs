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
#pragma warning restore SA1602
}