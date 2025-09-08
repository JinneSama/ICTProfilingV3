using Models.Enums;
using System.Threading.Tasks;

namespace ICTProfilingV3.Interfaces
{
    public interface IProcessService
    {
        Task AddProcessLog(int processId, RequestType requestType, TicketStatus status);
        Task<TicketStatus?> GetProcessStatus(int processId, RequestType requestType);
        Task UpdateProcessStatus(int processId, RequestType requestType, TicketStatus status);
    }
}
