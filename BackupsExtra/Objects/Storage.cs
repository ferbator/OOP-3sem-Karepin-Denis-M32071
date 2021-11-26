using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupsExtra.Tools;

namespace BackupsExtra.Objects
{
    [Serializable]
    public class Storage
    {
        private readonly string _path;
        private List<JobObject> _jobObjects;
        public Storage(string path)
        {
            if (path == null) throw new BackupsExtraException("Incorrect path");
            var imaginaryFile = new FileInfo(path);
            Name = imaginaryFile.Name;
            Length = imaginaryFile.Length;
            _path = path;
            TimeCreate = DateTime.Now;
            _jobObjects = new List<JobObject>();
        }

        public string Name { get; }
        public double Length { get; }
        public DateTime TimeCreate { get; }
        public List<JobObject> GetJobObjects => _jobObjects.ToList();

        public void AddJobObject(JobObject jobObject)
        {
            _jobObjects.Add(jobObject);
        }

        public void AddJobObjects(List<JobObject> jobObjects)
        {
            _jobObjects = jobObjects;
        }

        public string GetPath()
        {
            return _path;
        }
    }
}