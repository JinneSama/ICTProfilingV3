using Models.Enums;
using System.Threading.Tasks;

namespace Models.Managers
{
    public interface ITicketStatus
    {
        Task ModifyStatus(TicketStatus status, int ticketId);
    }
}
