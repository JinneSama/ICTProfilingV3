using ICTProfilingV3.Core.Common;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using Models.Enums;
using System;
using System.Threading.Tasks;

namespace ICTProfilingV3.Services
{
    public class ProcessService : IProcessService
    {
        private readonly IRepository<int, RecordsRequestStatus> _recordsRepo;
        private readonly IRepository<int, TicketRequestStatus> _ticketStatusRepo;
        private readonly IRepository<int, TicketRequest> _ticketRepo;
        private readonly IRepository<int, PurchaseRequest> _prRepo;
        private readonly IRepository<int, CustomerActionSheet> _casRepo;
        private readonly IRepository<int, MOAccountUsers> _moRepo;
        private readonly IRepository<int, PGNRequests> _pgnRepo;

        private readonly UserStore _userStore;

        public ProcessService(IRepository<int, RecordsRequestStatus> recordsRepo, UserStore userStore,
            IRepository<int, TicketRequestStatus> ticketStatusRepo, IRepository<int, TicketRequest> ticketRepo,
             IRepository<int, PurchaseRequest> prRepo, IRepository<int, CustomerActionSheet> casRepo,
             IRepository<int, MOAccountUsers> moRepo, IRepository<int, PGNRequests> pgnRepo)
        {
            _recordsRepo = recordsRepo;
            _userStore = userStore;
            _ticketStatusRepo = ticketStatusRepo;
            _ticketRepo = ticketRepo;
            _prRepo = prRepo;
            _casRepo = casRepo;
            _moRepo = moRepo;
            _pgnRepo = pgnRepo;
        }
        public async Task AddProcessLog(int processId, RequestType requestType, TicketStatus status)
        {
            if(requestType == RequestType.TechSpecs || requestType == RequestType.Deliveries || requestType == RequestType.Repairs)
            {
                var ticketStatus = new TicketRequestStatus
                {
                    Status = status,
                    DateStatusChanged = DateTime.Now,
                    ChangedByUserId = _userStore.UserId,
                    TicketRequestId = processId
                };
                await _ticketStatusRepo.AddAsync(ticketStatus);
                return;
            }

            var recordStatus = new RecordsRequestStatus
            {
                Status = status,
                DateStatusChanged = DateTime.Now,
                ChangedByUserId = _userStore.UserId
            };

            if (requestType == RequestType.PR) recordStatus.PRId = processId;
            if (requestType == RequestType.CAS) recordStatus.CASId = processId;
            if (requestType == RequestType.M365) recordStatus.MOId = processId;
            if (requestType == RequestType.PGN) recordStatus.PGNId = processId;
            await _recordsRepo.AddAsync(recordStatus);
        }

        public async Task<TicketStatus?> GetProcessStatus(int processId, RequestType requestType)
        {
            TicketStatus? status = default;
            if(requestType == RequestType.TechSpecs || requestType == RequestType.Deliveries || requestType == RequestType.Repairs)
            {
                status = (await _ticketRepo.GetById(processId)).TicketStatus;
            }
            if (requestType == RequestType.PR)
            {
                status = (await _prRepo.GetById(processId)).Status;
            }
            if (requestType == RequestType.CAS)
            {
                status = (await _casRepo.GetById(processId)).Status;
            }
            if (requestType == RequestType.M365)
            {
                status = (await _moRepo.GetById(processId)).Status;
            }
            if (requestType == RequestType.PGN)
            {
                status = (await _pgnRepo.GetById(processId)).Status;
            }

            return status;
        }

        public async Task UpdateProcessStatus(int processId, RequestType requestType, TicketStatus status)
        {
            if (requestType == RequestType.TechSpecs || requestType == RequestType.Deliveries || requestType == RequestType.Repairs)
            {
                var ticket = await _ticketRepo.GetById(processId);
                ticket.TicketStatus = status;
                await _ticketRepo.SaveChangesAsync();
            }
            if (requestType == RequestType.PR)
            {
                var process = await _prRepo.GetById(processId);
                process.Status = status;
                await _prRepo.SaveChangesAsync();
            }
            if (requestType == RequestType.CAS)
            {
                var process = await _casRepo.GetById(processId);
                process.Status = status;
                await _casRepo.SaveChangesAsync();
            }
            if (requestType == RequestType.M365)
            {
                var process = await _moRepo.GetById(processId);
                process.Status = status;
                await _moRepo.SaveChangesAsync();
            }
            if (requestType == RequestType.PGN)
            {
                var process = await _pgnRepo.GetById(processId);
                process.Status = status;
                await _pgnRepo.SaveChangesAsync();
            }
        }
    }
}
