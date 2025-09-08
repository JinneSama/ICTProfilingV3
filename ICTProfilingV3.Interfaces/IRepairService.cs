using ICTProfilingV3.DataTransferModels.ViewModels;
using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICTProfilingV3.Interfaces
{
    public interface IRepairService : IBaseDataService<Repairs, int>
    {
        IEnumerable<PPEsViewModel> GetRepairPPE();
        Task<PPEs> GetPPE(int PPEId);
        IEnumerable<RepairViewModel> GetRepairViewModels();
    }
}
