using BackupsExtra.Services;

namespace BackupsExtra.Logger
{
    public interface ILogsBackups
    {
        void Log(TypeOfLogging typeOfLogging, string massage);
    }
}