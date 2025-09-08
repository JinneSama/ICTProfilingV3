using ICTProfilingV3.DataTransferModels;
using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICTProfilingV3.Interfaces
{
    public interface ICASService : IBaseDataService<CustomerActionSheet, int>
    {
        Task UpdateCAS(CASDetailDTM cas, int casId);
        Task AddCAS(CASDetailDTM cas);
        Task<CASDetailDTM> GetCASDetail(int casId);
        IEnumerable<CASDTM> GetCASDTM();
    }
}
