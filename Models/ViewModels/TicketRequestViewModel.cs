using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using System.Data.Entity.Core.Metadata.Edm;

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
        public string RequestedBy => employee.Employee;
        public string Office => employee.Office + " " + employee.Division;
        private void SetEmployee(TicketRequest ticket)
        {
            long? reqById = null;
            if (ticket.Deliveries != null) reqById = ticket.Deliveries.RequestedById;
            if (ticket.TechSpecs != null) reqById = ticket.TechSpecs.ReqById;
            if (ticket.Repairs != null) reqById = ticket.Repairs.RequestedById;
            
            if(reqById != null) employee = HRMISEmployees.GetEmployeeById(reqById);
        }
    }
}
