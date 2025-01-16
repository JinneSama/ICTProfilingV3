using Models.Entities;
using Models.HRMISEntites;
using System.Net.Sockets;

namespace Models.ViewModels
{
    public class TasksViewModel
    {
        private EmployeesViewModel employee;
        private TicketRequest _ticket;
        public TicketRequest Ticket
        {
            get { return _ticket; }
            set
            {
                _ticket = value;
                SetEmployee();
            }
        }

        public string EPiSNo =>  "EPiS-" + _ticket.Id.ToString();
        public string Status { get; set; }
        public string ReqByName => employee?.Employee;
        public string ReqByPos => employee?.Position;
        public string Office => employee?.Office;
        private void SetEmployee()
        {
            long? reqById = null;
            if (_ticket.Deliveries != null) reqById = _ticket.Deliveries.RequestedById;
            if (_ticket.TechSpecs != null) reqById = _ticket.TechSpecs.ReqById;
            if (_ticket.Repairs != null) reqById = _ticket.Repairs.RequestedById;

            if(reqById != null) employee = HRMISEmployees.GetEmployeeById(reqById);
        }
    }
}
