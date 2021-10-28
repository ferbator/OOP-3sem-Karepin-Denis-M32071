using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Backups.Objects
{
    public class AlgoSingleStorage
    {
        private readonly string _pathToBackupTmpFolderForSingleAlgo;

        public AlgoSingleStorage(string path)
        {
            _pathToBackupTmpFolderForSingleAlgo = path;
        }

        public List<Storage> DoAlgorithmic(List<JobObject> repo, int launchNumber)
        {
            string pathForAuxDirectory = $"{_pathToBackupTmpFolderForSingleAlgo}\\AllFile_{launchNumber}";
            Directory.CreateDirectory(pathForAuxDirectory);
            var tmpListStorage = new List<Storage>();
            foreach (JobObject imgFile in repo)
            {
                string path =
                    $"{pathForAuxDirectory}\\{imgFile.Name.Split(char.Parse("."))[0]}_{launchNumber}.{imgFile.Name.Split(char.Parse("."))[1]}";
                var fileInf = new FileInfo(imgFile.GetPath());
                if (fileInf.Exists)
                {
                    fileInf.CopyTo(path);
                }

                tmpListStorage.Add(new Storage(path));
            }

            ZipFile.CreateFromDirectory(pathForAuxDirectory, pathForAuxDirectory + ".zip");
            Directory.Delete(pathForAuxDirectory, true);
            return tmpListStorage;
        }
    }
}