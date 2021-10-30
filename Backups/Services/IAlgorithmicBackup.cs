using System.Collections.Generic;
using Backups.Objects;

namespace Backups.Services
{
    public interface IAlgorithmicBackup
    {
        IEnumerable<Storage> DoAlgorithmic(List<JobObject> repo, int launchNumber);
    }
}