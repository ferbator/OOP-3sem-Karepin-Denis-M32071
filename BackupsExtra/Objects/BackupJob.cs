using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using BackupsExtra.Logger;
using BackupsExtra.Objects.Algorithms;
using BackupsExtra.Services;
using BackupsExtra.Tools;

namespace BackupsExtra.Objects
{
    public class BackupJob
    {
        private static int _counter;
        private List<RestorePoint> _restorePoint = new List<RestorePoint>();

        private string _defaultPathToBackupFolder =
            @"C:\Users\HTMLD\Documents\GitHub\ferbator\BackupsExtra\Zone Backup";

        private string _defaulPathToBackupTmpFolder =
            @"C:\Users\HTMLD\Documents\GitHub\ferbator\BackupsExtra\Zone Tmp File";

        private List<JobObject> _jobObjects = new List<JobObject>();

        private IRepository _repository;
        private ILogsBackups _logsBackups;

        public BackupJob(OptionsForLogging optionForLogging, bool configTimeCode)
        {
            _logsBackups = optionForLogging switch
            {
                OptionsForLogging.ToConsole => new LoggingConsole(configTimeCode),
                OptionsForLogging.ToFile => new LoggingFile(configTimeCode),
                _ => null
            };
            AddAllFilesFromWorkingDirectoryToQueue();
            _logsBackups?.Log(TypeOfLogging.Info, $"Scan of file in {_defaultPathToBackupFolder}");
            _repository = new Repository(_defaulPathToBackupTmpFolder);
            _logsBackups?.Log(TypeOfLogging.Info, $"Create abstract repo from {_defaulPathToBackupTmpFolder}");
        }

        public BackupJob(string path, OptionsForLogging optionForLogging, bool configTimeCode)
        {
            _logsBackups = optionForLogging switch
            {
                OptionsForLogging.ToConsole => new LoggingConsole(configTimeCode),
                OptionsForLogging.ToFile => new LoggingFile(configTimeCode),
                _ => null
            };
            if (path == null) throw new BackupsExtraException("Incorrect path");
            if (_defaulPathToBackupTmpFolder != path)
                _defaulPathToBackupTmpFolder = path;
            AddAllFilesFromWorkingDirectoryToQueue();
            _logsBackups?.Log(TypeOfLogging.Info, $"Scan of file in {_defaultPathToBackupFolder}");
            _repository = new Repository(path);
            _logsBackups?.Log(TypeOfLogging.Info, $"Create abstract repo from {_defaulPathToBackupTmpFolder}");
        }

        public BackupJob(string pathFrom, string pathTo, OptionsForLogging optionForLogging, bool configTimeCode)
        {
            _logsBackups = optionForLogging switch
            {
                OptionsForLogging.ToConsole => new LoggingConsole(configTimeCode),
                OptionsForLogging.ToFile => new LoggingFile(configTimeCode),
                _ => null
            };
            if (pathFrom == null || pathTo == null) throw new BackupsExtraException("Incorrect path");
            if (_defaultPathToBackupFolder != pathFrom && _defaulPathToBackupTmpFolder != pathTo)
            {
                _defaultPathToBackupFolder = pathFrom;
                _defaulPathToBackupTmpFolder = pathTo;
            }

            _repository = new Repository(pathTo);
            _logsBackups?.Log(TypeOfLogging.Info, $"Create abstract repo from {_defaulPathToBackupTmpFolder}");
            AddAllFilesFromWorkingDirectoryToQueue();
            _logsBackups?.Log(TypeOfLogging.Info, $"Scan of file in {_defaultPathToBackupFolder}");
        }

        public void DeleteJobObjectInQueueBackup(string name)
        {
            if (name == null) throw new BackupsExtraException("Incorrect name file");
            foreach (JobObject jobObject in _jobObjects.Where(jobObject => jobObject.Name == name))
            {
                _jobObjects.Remove(jobObject);
                _logsBackups.Log(TypeOfLogging.Info, $"Delete file - {jobObject.Name} of queue Backup");
                break;
            }
        }

        public void AddJobObjectInQueueBackup(string name)
        {
            if (name == null) throw new BackupsExtraException("Incorrect name file");
            string path = $"{_defaultPathToBackupFolder}\\{name}";
            var fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                _jobObjects.Add(new JobObject(path));
                _logsBackups.Log(TypeOfLogging.Info, $"Add file - {name} of queue Backup");
            }
        }

        public RestorePoint LaunchBackup(OptionsForBackup option)
        {
            _counter++;
            switch (option)
            {
                case OptionsForBackup.SingleStorage:
                    _logsBackups.Log(TypeOfLogging.Info, $"Set {option} for Backup");
                    IAlgorithmicBackup algoFirst = new AlgoSingleStorage(_defaulPathToBackupTmpFolder);
                    var tmpStoragesFirst = (List<Storage>)algoFirst.DoAlgorithmic(_jobObjects, _counter);
                    _repository.AddStoragesToRepo(tmpStoragesFirst);
                    _restorePoint.Add(new RestorePoint(tmpStoragesFirst));
                    break;

                case OptionsForBackup.SplitStorages:
                    _logsBackups.Log(TypeOfLogging.Info, $"Set {option} for Backup");
                    IAlgorithmicBackup algoSecond = new AlgoSplitStorages(_defaulPathToBackupTmpFolder);
                    var tmpStoragesSecond = (List<Storage>)algoSecond.DoAlgorithmic(_jobObjects, _counter);
                    _repository.AddStoragesToRepo(tmpStoragesSecond);
                    _restorePoint.Add(new RestorePoint(tmpStoragesSecond));
                    break;
                default:
                    throw new BackupsExtraException($"{option} - Incorrect options");
            }

            return _restorePoint[^1];
        }

        public bool CheckFileInListJobObjects(string name)
        {
            if (name == null) throw new BackupsExtraException("Incorrect name file");
            return _jobObjects.SingleOrDefault(jobObject => jobObject.Name == name) != null;
        }

        public void ClearTmpRepo()
        {
            _logsBackups.Log(TypeOfLogging.Info, $"Clear all file in \n{_repository.GetPath()}");
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

        public void RestoringFilesFromRestorePoint(RestorePoint restorePoint, string path, OptionsForRestoringFiles option)
        {
            switch (option)
            {
                case OptionsForRestoringFiles.ToOriginalLocation:
                    _logsBackups.Log(TypeOfLogging.Info, $"Set {option} for Restoring Files");
                    if (!string.IsNullOrWhiteSpace(path)) break;
                    foreach (Storage storage in restorePoint.GetStorages)
                    {
                        foreach (JobObject jobObject in storage.GetJobObjects.Where(jobObject => File.Exists(jobObject.GetPath())))
                        {
                            File.Delete(jobObject.GetPath());
                            _logsBackups.Log(TypeOfLogging.Info, $"Delete file - {jobObject.Name}");
                        }

                        ZipFile.ExtractToDirectory(storage.GetPath(), _defaultPathToBackupFolder);
                    }

                    break;
                case OptionsForRestoringFiles.ToDifferentLocation:
                    _logsBackups.Log(TypeOfLogging.Info, $"Set {option} for Restoring Files");
                    foreach (Storage storage in restorePoint.GetStorages)
                    {
                        foreach (JobObject jobObject in storage.GetJobObjects.Where(jobObject => File.Exists(jobObject.GetPath())))
                        {
                            File.Delete(Path.Combine(path, jobObject.Name));
                            _logsBackups.Log(TypeOfLogging.Info, $"Delete file - {jobObject.Name}");
                        }

                        _logsBackups.Log(TypeOfLogging.Info, $"Extract Archive \nfrom {storage.GetPath()}\n to {path}");
                        ZipFile.ExtractToDirectory(storage.GetPath(), path);
                    }

                    break;
                default:
                    throw new BackupsExtraException($"{option} - Incorrect options");
            }
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