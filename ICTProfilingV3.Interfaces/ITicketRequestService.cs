using ICTProfilingV3.DataTransferModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICTProfilingV3.Interfaces
{
    public interface ITicketRequestService
    {
        Task<IEnumerable<TicketRequestDTM>> GetTicketRequests();
    }
}
