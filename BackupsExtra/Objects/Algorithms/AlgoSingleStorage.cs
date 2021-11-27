using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using BackupsExtra.Services;
using BackupsExtra.Tools;

namespace BackupsExtra.Objects.Algorithms
{
    public class AlgoSingleStorage : IAlgorithmicBackup
    {
        private readonly string _pathToBackupTmpFolderForSingleAlgo;

        public AlgoSingleStorage(string path)
        {
            _pathToBackupTmpFolderForSingleAlgo = path;
        }

        public IEnumerable<Storage> DoAlgorithmic(List<JobObject> repo, int launchNumber)
        {
            var tmpListStorage = new List<Storage>();
            string pathForAuxDirectory = Path.Combine(_pathToBackupTmpFolderForSingleAlgo, $"AllFile_{launchNumber}");
            Directory.CreateDirectory(pathForAuxDirectory);

            foreach (JobObject imgFile in repo)
            {
                string path = Path.Combine(pathForAuxDirectory, imgFile.Name);
                var fileInf = new FileInfo(imgFile.GetPath());
                if (fileInf.Exists)
                {
                    fileInf.CopyTo(path);
                }
                else
                {
                    Directory.Delete(pathForAuxDirectory, true);
                    throw new BackupsExtraException("The file does not exist");
                }
            }

            var zipInf = new FileInfo(pathForAuxDirectory + ".zip");
            if (!zipInf.Exists)
            {
                ZipFile.CreateFromDirectory(pathForAuxDirectory, pathForAuxDirectory + ".zip");
            }

            Directory.Delete(pathForAuxDirectory, true);
            var tmpStorage = new Storage(pathForAuxDirectory + ".zip", repo);
            tmpStorage.AddJobObjects(repo);
            tmpListStorage.Add(tmpStorage);

            return tmpListStorage;
        }
    }
}