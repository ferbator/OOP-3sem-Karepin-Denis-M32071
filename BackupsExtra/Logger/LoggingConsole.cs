using System;
using BackupsExtra.Services;

namespace BackupsExtra.Logger
{
    [Serializable]
    public class LoggingConsole : ILogsBackups
    {
        private readonly bool _time;
        public LoggingConsole(bool flag)
        {
            _time = flag;
        }

        public void Log(TypeOfLogging typeOfLogging, string message)
        {
            Console.WriteLine(
                _time ? $"{typeOfLogging} -- {message}" : $"{DateTime.Now} -- {typeOfLogging} -- {message}");
        }
    }
}