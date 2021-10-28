using System.Collections.Generic;
using System.IO;
using Backups.Tools;

namespace Backups.Objects
{
    public class JobObject
    {
        private readonly string _path;
        public JobObject(string path)
        {
            if (path == null) throw new BackupException("Incorrect path");
            var imaginaryFile = new FileInfo(path);
            Name = imaginaryFile.Name;
            Length = imaginaryFile.Length;
            _path = path;
        }

        public string Name { get; }
        public double Length { get; }

        public string GetPath()
        {
            return _path;
        }
    }
}