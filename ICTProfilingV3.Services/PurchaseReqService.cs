using ICTProfilingV3.Core.Common;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Base;
using Models.Entities;
using System;
using System.Threading.Tasks;

namespace ICTProfilingV3.Services
{
    public class PurchaseReqService : BaseDataService<PurchaseRequest, int>, IPurchaseReqService
    {
        private readonly UserStore _userStore;
        private readonly IDocActionsService _actionService;
        private readonly IRepository<int, RecordsRequestStatus> _recordsService;
        public PurchaseReqService(IRepository<int, PurchaseRequest> baseRepo, UserStore userStore,
            IDocActionsService actionService, IRepository<int, RecordsRequestStatus> recordsService) : base(baseRepo)
        {
            _userStore = userStore;
            _actionService = actionService;
            _recordsService = recordsService;
        }

        public override async Task DeleteAsync(int id)
        {
            await _actionService.DeleteRangeAsync(x => x.PurchaseRequestId == id);
            _recordsService.DeleteRange(x => x.PRId == id);
            await _recordsService.SaveChangesAsync();
            await base.DeleteAsync(id);
        }
        public override Task<PurchaseRequest> AddAsync(PurchaseRequest entity)
        {
            entity.CreatedById = _userStore.UserId;
            entity.DateCreated = DateTime.Now;
            return base.AddAsync(entity);
        }
    }
}
