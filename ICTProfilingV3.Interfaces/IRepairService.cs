using ICTProfilingV3.DataTransferModels.ViewModels;
using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICTProfilingV3.Interfaces
{
    public interface IRepairService
    {
        IEnumerable<PPEsViewModel> GetRepairPPE();
        Task<PPEs> GetPPE(int PPEId);
        Task<Repairs> GetRepair(int repairId);
        Task SaveRepairChangesAsync();
    }
}
