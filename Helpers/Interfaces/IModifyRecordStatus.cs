using Models.Enums;
using System.Threading.Tasks;

namespace Helpers.Interfaces
{
    public interface IModifyRecordStatus
    {
        Task ModifyRecordStatus(TicketStatus status, RequestType type, int Id);
    }
}
