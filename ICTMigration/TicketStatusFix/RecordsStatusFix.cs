using ICTMigration.ICTv2Models;
using Models.Entities;
using Models.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace ICTMigration.TicketStatusFix
{
    public class RecordsStatusFix
    {
        private readonly ICTv2Entities ictv2Model;
        private readonly IUnitOfWork unitOfWork;
        public RecordsStatusFix()
        {
            ictv2Model = new ICTv2Entities();
            unitOfWork = new UnitOfWork();
        }

        public async Task CASFix()
        {
            var cas = unitOfWork.CustomerActionSheetRepo.GetAll(x => x.Actions);

            //Accepted
            foreach (var action in cas)
            {
                var firstAction = action.Actions.OrderBy(o => o.DateCreated).FirstOrDefault();
                if (firstAction == null) continue;

                var recordFix = new RecordsRequestStatus
                {
                    Status = Models.Enums.TicketStatus.Accepted,
                    DateStatusChanged = firstAction.DateCreated,
                    CASId = action.Id,
                    ChangedByUserId = action.CreatedById
                };
                unitOfWork.RecordsRequestStatus.Insert(recordFix);
            }

            //Completed
            foreach (var action in cas)
            {
                var firstAction = action.Actions.OrderByDescending(o => o.DateCreated).FirstOrDefault();
                if (firstAction == null) continue;

                var recordFix = new RecordsRequestStatus
                {
                    Status = Models.Enums.TicketStatus.Completed,
                    DateStatusChanged = firstAction.DateCreated,
                    CASId = action.Id,
                    ChangedByUserId = action.CreatedById
                };
                unitOfWork.RecordsRequestStatus.Insert(recordFix);
            }
            await unitOfWork.SaveChangesAsync();

            var uow = new UnitOfWork();
            foreach (var action in cas)
            {
                if(action.Status != null) continue;

                var currentCAS = await uow.CustomerActionSheetRepo.FindAsync(x => x.Id == action.Id);
                currentCAS.Status = Models.Enums.TicketStatus.Completed;
            }
            await uow.SaveChangesAsync();
        }

        public async Task PRFix()
        {
            var pr = unitOfWork.PurchaseRequestRepo.GetAll(x => x.Actions);

            //Accepted
            foreach (var action in pr)
            {
                var firstAction = action.Actions.OrderBy(o => o.DateCreated).FirstOrDefault();
                if (firstAction == null) continue;

                var recordFix = new RecordsRequestStatus
                {
                    Status = Models.Enums.TicketStatus.Accepted,
                    DateStatusChanged = firstAction.DateCreated,
                    PRId = action.Id,
                    ChangedByUserId = action.CreatedById
                };
                unitOfWork.RecordsRequestStatus.Insert(recordFix);
            }

            //Completed
            foreach (var action in pr)
            {
                var firstAction = action.Actions.OrderByDescending(o => o.DateCreated).FirstOrDefault();
                if (firstAction == null) continue;

                var recordFix = new RecordsRequestStatus
                {
                    Status = Models.Enums.TicketStatus.Completed,
                    DateStatusChanged = firstAction.DateCreated,
                    PRId = action.Id,
                    ChangedByUserId = action.CreatedById
                };
                unitOfWork.RecordsRequestStatus.Insert(recordFix);
            }
            await unitOfWork.SaveChangesAsync();

            var uow = new UnitOfWork();
            foreach (var action in pr)
            {
                if (action.Status != null) continue;

                var currentPR = await uow.PurchaseRequestRepo.FindAsync(x => x.Id == action.Id);
                currentPR.Status = Models.Enums.TicketStatus.Completed;
            }
            await uow.SaveChangesAsync();
        }
    }
}
