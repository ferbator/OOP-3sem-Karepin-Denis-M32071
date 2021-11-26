using System;
using System.IO;
using BackupsExtra.Services;

namespace BackupsExtra.Logger
{
    public class LoggingFile : ILogsBackups
    {
        private readonly bool _time;
        public LoggingFile(bool flag)
        {
            _time = flag;
        }

        public void Log(TypeOfLogging typeOfLogging, string message)
        {
            File.AppendAllLines(
                @"C:\Users\HTMLD\Documents\GitHub\ferbator\BackupsExtra\Logger\Logs.txt",
                _time ? new[] { $"{DateTime.Now} -- {typeOfLogging} -- {message}\n" } : new[] { $"{typeOfLogging} -- {message}\n" });
        }
    }
}