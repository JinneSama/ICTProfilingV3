using ICTProfilingV3.DataTransferModels;
using Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICTProfilingV3.Interfaces
{
    public interface ITicketRequestService : IBaseDataService<TicketRequest, int>
    {
        IBaseDataService<TicketRequestStatus, int> TicketRequestStatusService { get; set; }
        Task<IEnumerable<TicketRequestDTM>> GetTicketRequests();
        Task<bool> CanAssignTicket();
    }
}
