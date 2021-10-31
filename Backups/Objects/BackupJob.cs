using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Services;
using Backups.Tools;

namespace Backups.Objects
{
    public class BackupJob
    {
        private static int _counter;
        private List<RestorePoint> _restorePoint = new List<RestorePoint>();

        private string _defaultPathToBackupFolder =
            @"C:\Users\HTMLD\Documents\GitHub\ferbator\Backups\Zone Backup";

        private string _defaulPathToBackupTmpFolder =
            @"C:\Users\HTMLD\Documents\GitHub\ferbator\Backups\Zone Tmp File";

        private List<JobObject> _jobObjects = new List<JobObject>();

        private IRepository _repository;

        public BackupJob()
        {
            AddAllFilesFromWorkingDirectoryToQueue();
            _repository = new Repository(_defaulPathToBackupTmpFolder);
        }

        public BackupJob(string path)
        {
            if (path == null) throw new BackupException("Incorrect path");
            if (_defaulPathToBackupTmpFolder != path)
                _defaulPathToBackupTmpFolder = path;
            AddAllFilesFromWorkingDirectoryToQueue();
            _repository = new Repository(path);
        }

        public BackupJob(string pathFrom, string pathTo)
        {
            if (pathFrom == null || pathTo == null) throw new BackupException("Incorrect path");
            if (_defaultPathToBackupFolder != pathFrom && _defaulPathToBackupTmpFolder != pathTo)
            {
                _defaultPathToBackupFolder = pathFrom;
                _defaulPathToBackupTmpFolder = pathTo;
            }

            _repository = new Repository(pathTo);

            AddAllFilesFromWorkingDirectoryToQueue();
        }

        public void DeleteJobObjectInQueueBackup(string name)
        {
            if (name == null) throw new BackupException("Incorrect name file");
            foreach (JobObject jobObject in _jobObjects.Where(jobObject => jobObject.Name == name))
            {
                _jobObjects.Remove(jobObject);
                break;
            }
        }

        public void AddJobObjectInQueueBackup(string name)
        {
            if (name == null) throw new BackupException("Incorrect name file");
            string path = $"{_defaultPathToBackupFolder}\\{name}";
            var fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                _jobObjects.Add(new JobObject(path));
            }
        }

        public void LaunchBackup(OptionsForBackup option)
        {
            _counter++;
            switch (option)
            {
                case OptionsForBackup.SingleStorage:
                    IAlgorithmicBackup algoFirst = new AlgoSingleStorage(_defaulPathToBackupTmpFolder);
                    var tmpStoragesFirst = (List<Storage>)algoFirst.DoAlgorithmic(_jobObjects, _counter);
                    _repository.AddStoragesToRepo(tmpStoragesFirst);
                    _restorePoint.Add(new RestorePoint(tmpStoragesFirst));
                    break;

                case OptionsForBackup.SplitStorages:
                    IAlgorithmicBackup algoSecond = new AlgoSplitStorages(_defaulPathToBackupTmpFolder);
                    var tmpStoragesSecond = (List<Storage>)algoSecond.DoAlgorithmic(_jobObjects, _counter);
                    _repository.AddStoragesToRepo(tmpStoragesSecond);
                    _restorePoint.Add(new RestorePoint(tmpStoragesSecond));
                    break;
                default:
                    throw new BackupException($"{option} - Incorrect options");
            }
        }

        public bool CheckFileInListJobObjects(string name)
        {
            if (name == null) throw new BackupException("Incorrect name file");
            return _jobObjects.SingleOrDefault(jobObject => jobObject.Name == name) != null;
        }

        public void ClearTmpRepo()
        {
            _repository.ClearRepo();
        }

        public int CountRestorePoint()
        {
            return _restorePoint.Count;
        }

        public int CountStoragesInRepo()
        {
            return _repository.CountStorages();
        }

        private void AddAllFilesFromWorkingDirectoryToQueue()
        {
            var pathsOfFiles = new List<string>(Directory.GetFiles(_defaultPathToBackupFolder));
            var tmpListWithJobObjectsInZoneBackup =
                new List<JobObject>(pathsOfFiles.Select(path => new JobObject(path)));
            _jobObjects = tmpListWithJobObjectsInZoneBackup;
        }
    }
}