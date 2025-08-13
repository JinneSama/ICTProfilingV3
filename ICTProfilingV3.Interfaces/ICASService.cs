using ICTProfilingV3.DataTransferModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICTProfilingV3.Interfaces
{
    public interface ICASService
    {
        Task UpdateCAS(CASDetailDTM cas, int casId);
        Task AddCAS(CASDetailDTM cas);
        Task<CASDetailDTM> GetCASDetail(int casId);
        IEnumerable<CASDTM> GetCASDTM();
    }
}
