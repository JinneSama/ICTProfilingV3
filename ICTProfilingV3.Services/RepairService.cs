using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Employees;
using Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.Services
{
    public class RepairService : IRepairService
    {
        private readonly IRepository<int, PPEs> _ppeRepo;
        private readonly IRepository<int, Repairs> _repairRepo;
        public RepairService(IRepository<int, PPEs> ppeRepo, IRepository<int, Repairs> repairRepo)
        {
            _ppeRepo = ppeRepo;
            _repairRepo = repairRepo;
        }

        public async Task<PPEs> GetPPE(int PPEId)
        {
            return await _ppeRepo.GetById(PPEId);
        }

        public async Task<Repairs> GetRepair(int repairId)
        {
            return await _repairRepo.GetById(repairId);
        }

        public IEnumerable<PPEsViewModel> GetRepairPPE()
        {
            var ppes = _ppeRepo.GetAll().ToList();
            var ppeViewModels = ppes.Select(x => new PPEsViewModel
            {
                Id = x.Id,
                PropertyNo = x.PropertyNo,
                IssuedTo = HRMISEmployees.GetEmployeeById(x.IssuedToId)?.Employee,
                DateCreated = x.DateCreated,
                Office = HRMISEmployees.GetEmployeeById(x.IssuedToId)?.Office,
                Status = x?.Status
            });
            return ppeViewModels;
        }

        public async Task SaveRepairChangesAsync()
        {
            await _repairRepo.SaveChangesAsync();
        }
    }
}
