using System;
using System.IO;
using BackupsExtra.Tools;

namespace BackupsExtra.Objects
{
    [Serializable]
    public class JobObject
    {
        private readonly string _path;
        public JobObject(string path)
        {
            if (path == null) throw new BackupsExtraException("Incorrect path");
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