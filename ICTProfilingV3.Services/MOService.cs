using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Base;
using Models.Entities;

namespace ICTProfilingV3.Services
{
    public class MOService : BaseDataService<MOAccounts, int>, IMOService
    {
        public MOService(IRepository<int, MOAccounts> baseRepo, IBaseDataService<MOAccountUsers, int> baseDataService) : base(baseRepo)
        {
            MOAccountUserBaseService = baseDataService;
        }
        public IBaseDataService<MOAccountUsers, int> MOAccountUserBaseService { get; set; }
    }
}
