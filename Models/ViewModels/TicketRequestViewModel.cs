using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace Models.ViewModels
{
    public class TicketRequestViewModel
    {
        private EmployeesViewModel employee;
        private TicketRequest _ticketRequest;
        public TicketRequest TicketRequest
        {
            get { return this._ticketRequest; }
            set
            {
                _ticketRequest = value;
                SetEmployee(_ticketRequest);
            }
        }
        public string Status => EnumHelper.GetEnumDescription(_ticketRequest?.TicketStatus ?? TicketStatus.Accepted);
        public string TypeOfRequest => EnumHelper.GetEnumDescription(_ticketRequest.RequestType);
        public string RequestedBy => employee?.Employee;
        public string Office => employee?.Office + " " + employee?.Division;
        private void SetEmployee(TicketRequest ticket)
        {
            long? reqById = null;
            if (ticket.Deliveries != null) reqById = ticket.Deliveries.RequestedById;
            if (ticket.TechSpecs != null) reqById = ticket.TechSpecs.ReqById;
            if (ticket.Repairs != null) reqById = ticket.Repairs.RequestedById;

            if (reqById != null) employee = HRMISEmployees.GetEmployeeById(reqById);
        }

        private TicketInfo TicketInfo()
        {
            if(employee == null) return null;
            long? ChiefId = HRMISEmployees.GetChief(employee.Office, employee.Division)?.ChiefId;
            var chief = HRMISEmployees.GetEmployeeById(ChiefId);

            var staff = new StaffViewModel
            {
                Staff = TicketRequest?.ITStaff
            };
            var actions = GetActions();

            var actionsVM = new RoutedActionsViewModel
            {
                ActionDate = actions?.ActionDate,
                Remarks = actions?.Remarks,
                Actions = actions,
                From = actions?.CreatedBy?.UserName
            };

            var ticketInfo = new TicketInfo
            {
                Chief = chief,
                ITStaff = staff
            };
            return ticketInfo;
        }

        private Actions GetActions()
        {
            if (TicketRequest.RequestType == RequestType.TechSpecs)
                return TicketRequest.TechSpecs.Actions.LastOrDefault();
            if (TicketRequest.RequestType == RequestType.Deliveries)
                return TicketRequest.Deliveries.Actions.LastOrDefault();
            if (TicketRequest.RequestType == RequestType.Repairs)
                return TicketRequest.Repairs.Actions.LastOrDefault();
            return null;
        }

        public BindingList<TicketInfo> Info => new BindingList<TicketInfo>() { TicketInfo() };
    }
}
