using System.Collections.Generic;
using BackupsExtra.Objects;

namespace BackupsExtra.Services
{
    public interface IAlgorithmicBackup
    {
        IEnumerable<Storage> DoAlgorithmic(List<JobObject> repo, int launchNumber);
    }
}