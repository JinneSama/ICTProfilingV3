using Models.Entities;

namespace ICTProfilingV3.Interfaces
{
    public interface IMOService : IBaseDataService<MOAccounts, int>
    {
        IBaseDataService<MOAccountUsers, int> MOAccountUserBaseService { get; set; }
    }
}
