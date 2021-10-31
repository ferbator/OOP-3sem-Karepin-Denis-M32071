using System;
using System.IO;
using Backups.Tools;

namespace Backups.Objects
{
    public class Storage
    {
        private readonly string _path;
        public Storage(string path)
        {
            if (path == null) throw new BackupException("Incorrect path");
            var imaginaryFile = new FileInfo(path);
            Name = imaginaryFile.Name;
            Length = imaginaryFile.Length;
            _path = path;
            TimeCreate = DateTime.Now;
        }

        public string Name { get; }
        public double Length { get; }

        public DateTime TimeCreate { get; }

        public string GetPath()
        {
            return _path;
        }
    }
}