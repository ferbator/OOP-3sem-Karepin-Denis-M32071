using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using BackupsExtra.Services;
using BackupsExtra.Tools;

namespace BackupsExtra.Objects.Algorithms
{
    public class AlgoSplitStorages : IAlgorithmicBackup
    {
        private readonly string _pathToBackupTmpFolderForSingleAlgo;

        public AlgoSplitStorages(string path)
        {
            _pathToBackupTmpFolderForSingleAlgo = path;
        }

        public IEnumerable<Storage> DoAlgorithmic(List<JobObject> repo, int launchNumber)
        {
            var tmpListStorage = new List<Storage>();
            foreach (JobObject imgFile in repo)
            {
                string pathForAuxDirectory = Path.Combine(_pathToBackupTmpFolderForSingleAlgo, $"{imgFile.Name.Split(char.Parse("."))[0]}_{launchNumber}");
                Directory.CreateDirectory(pathForAuxDirectory);
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

                var zipInf = new FileInfo(pathForAuxDirectory + ".zip");
                if (!zipInf.Exists)
                {
                    ZipFile.CreateFromDirectory(pathForAuxDirectory, pathForAuxDirectory + ".zip");
                }

                var tmpStorage = new Storage(pathForAuxDirectory + ".zip");
                tmpStorage.AddJobObject(imgFile);
                tmpListStorage.Add(tmpStorage);
                Directory.Delete(pathForAuxDirectory, true);
            }

            return tmpListStorage;
        }
    }
}