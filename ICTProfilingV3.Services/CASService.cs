using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Base;
using ICTProfilingV3.Services.Employees;
using Models.Entities;
using Models.Enums;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.Services
{
    public class CASService : BaseDataService<CustomerActionSheet, int>, ICASService
    {
        private readonly IEvaluationService _evaluationService;
        private readonly IDocActionsService _actionService;
        private readonly IRepository<int, RecordsRequestStatus> _recordRequestService;
        public CASService(IRepository<int, CustomerActionSheet> baseRepo, IDocActionsService actionService, 
            IEvaluationService evaluationService, IRepository<int, RecordsRequestStatus> recordRequestService) : base(baseRepo)
        {
            _actionService = actionService;
            _evaluationService = evaluationService;
            _recordRequestService = recordRequestService;
        }

        public override async Task DeleteAsync(int id)
        {
            await _actionService.DeleteRangeAsync(x => x.CustomerActionSheetId == id);
            await _evaluationService.DeleteRangeAsync(x => x.CustomerActionSheetId == id);
            _recordRequestService.DeleteRange(x => x.CASId == id);
            await _recordRequestService.SaveChangesAsync();
            await base.DeleteAsync(id);
        }
        public async Task AddCAS(CASDetailDTM cas)
        {
            var newCAS = new CustomerActionSheet
            {
                DateCreated = cas.DateCreated,
                ClientName = cas.ClientName,
                //ClientId = cas.Employee,
                Office = cas.Office,
                ContactNo = cas.ContactNo,
                Gender = (Gender)cas.Gender,
                ClientRequest = cas.ClientRequest,
                ActionTaken = cas.ActionTaken,
                AssistedById = cas.AssistedBy
            };
            await base.AddAsync(newCAS);
        }

        public async Task<CASDetailDTM> GetCASDetail(int casId)
        {
            var cas = await base.GetByIdAsync(casId);
            var data = new CASDetailDTM()
            {
                DateCreated = cas.DateCreated,
                ClientName = cas.ClientName,
                //Employee = cas.ClientId,
                Office = cas.Office,
                ContactNo = cas.ContactNo,
                Gender = (int)cas.Gender,
                ClientRequest = cas.ClientRequest,
                ActionTaken = cas.ActionTaken,
                AssistedBy = cas.AssistedById,
                AssistedByName = cas?.AssistedBy?.FullName ?? ""
            };
            return data;
        }

        public IEnumerable<CASDTM> GetCASDTM()
        {
            var res = base.GetAll()
                .Include(x => x.AssistedBy)
                .OrderByDescending(x => x.DateCreated).ToList();

            var cas = res.Select(x => new CASDTM
            {
                Id = x.Id,
                DateCreated = x.DateCreated ?? System.DateTime.MinValue,
                Office = x.ClientId == null ? x.Office : HRMISEmployees.GetEmployeeById(x.ClientId).Office,
                Request = x.ClientRequest,
                AssistedBy = x.AssistedBy?.FullName,
                CustomerActionSheet = x
            });

            return cas.ToList();
        }

        public async Task UpdateCAS(CASDetailDTM cas, int casId)
        {
            var casToUpdate = await base.GetByIdAsync(casId);
            casToUpdate.DateCreated = cas.DateCreated;
            casToUpdate.ClientName = cas.ClientName;
            //casToUpdate.ClientId = cas.Employee;
            casToUpdate.Office = cas.Office;
            casToUpdate.ContactNo = cas.ContactNo;
            casToUpdate.Gender = (Gender)cas.Gender;
            casToUpdate.ClientRequest = cas.ClientRequest;
            casToUpdate.ActionTaken = cas.ActionTaken;
            casToUpdate.AssistedById = cas.AssistedBy;

            await base.SaveChangesAsync();
        }
    }
}
