using Models.Enums;
using System.Threading.Tasks;

namespace Helpers.Interfaces
{
    public interface IModifyTicketStatus
    {
        Task ModifyTicketStatusStatus(TicketStatus status, int Id);
    }
}
