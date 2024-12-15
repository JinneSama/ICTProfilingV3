using Models.Entities;
using Models.HRMISEntites;
using System;

namespace Models.ViewModels
{
    public class QueueViewModel
    {
        private TicketRequest _ticket;
        private EmployeesViewModel _employee;
        public TicketRequest Ticket
        {
            get { return _ticket; }
            set 
            { 
                _ticket = value;
                SetEmployee();
            }
        }

        public EmployeesViewModel Employee => _employee;
        public string EPiSNo => "EPiS-" + _ticket.Id;
        public string Office => _employee.Office + " " + _employee.Division;
        public string AssignedTo => _ticket.ITStaff?.Users?.UserName ?? "Not Yet Assigned!";
        private void SetEmployee()
        {
            long? reqById = null;
            if (_ticket.Deliveries != null) reqById = _ticket.Deliveries.RequestedById;
            if (_ticket.TechSpecs != null) reqById = _ticket.TechSpecs.ReqById;
            if (_ticket.Repairs != null) reqById = _ticket.Repairs.RequestedById;

            if (reqById != null) _employee = HRMISEmployees.GetEmployeeById(reqById);
        }
    }
}
