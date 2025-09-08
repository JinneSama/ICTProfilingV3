using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Base;
using Models.Entities;

namespace ICTProfilingV3.Services
{
    public class ChangeLogService : DocumentService<ChangeLogs, int>, IChangeLogService
    {
        public ChangeLogService(IRepository<int, ChangeLogs> baseRepo, IHTTPNetworkFolder networkFolder, IScanDocument scanDocument) : base(baseRepo, networkFolder, scanDocument)
        {
        }
    }
}
