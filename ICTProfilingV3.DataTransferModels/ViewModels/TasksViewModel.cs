using Models.Entities;
using Models.Enums;
using System.Linq;
using System.Net.Sockets;

namespace ICTProfilingV3.DataTransferModels.ViewModels
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
        private string GetLastAction()
        {
            switch (Ticket.RequestType)
            {
                case RequestType.TechSpecs:
                    return $"{Ticket?.TechSpecs?.Actions?.LastOrDefault()?.ActionTaken}\n{Ticket?.TechSpecs?.Actions?.LastOrDefault()?.Remarks}";
                case RequestType.Deliveries:
                    return $"{Ticket?.Deliveries?.Actions?.LastOrDefault()?.ActionTaken}\n{Ticket?.TechSpecs?.Actions?.LastOrDefault()?.Remarks}";
                case RequestType.Repairs:
                    return $"{Ticket?.Repairs?.Actions?.LastOrDefault()?.ActionTaken}\n{Ticket?.TechSpecs?.Actions?.LastOrDefault()?.Remarks}";
                default: return null;
            }
        }
        public string AssignedTo => Ticket?.ITStaff?.Users?.UserName ?? "Not yet Assigned";
        public string LastAction => GetLastAction();
        private string Equipment()
        {
            string equipment = string.Empty;
            if (Ticket.RequestType == RequestType.Deliveries)
                equipment = string.Join(",", Ticket.Deliveries.DeliveriesSpecs.Select(x => $"{x.Quantity} {x?.Model?.Brand?.EquipmentSpecs?.Equipment?.EquipmentName}"));
            if (Ticket.RequestType == RequestType.TechSpecs)
                equipment = string.Join(",", Ticket.TechSpecs.TechSpecsICTSpecs.Select(x => $"{x.Quantity} {x?.EquipmentSpecs?.Equipment?.EquipmentName}"));
            if (Ticket.RequestType == RequestType.Repairs)
                equipment = string.Join(",", Ticket.Repairs.PPEs.PPEsSpecs.Select(x => $"{x.Quantity} {x?.Model?.Brand?.EquipmentSpecs?.Equipment?.EquipmentName}"));
            return equipment;
        }
        public string Equipments => Equipment();
        public string PropertyNo => GetPropertyNo();
        private string GetPropertyNo()
        {
            string propertyNo = Ticket?.Repairs?.PPEs?.PropertyNo ?? "";
            return propertyNo;
        }

        private void SetEmployee()
        {
            long? reqById = null;
            if (_ticket.Deliveries != null) reqById = _ticket.Deliveries.RequestedById;
            if (_ticket.TechSpecs != null) reqById = _ticket.TechSpecs.ReqById;
            if (_ticket.Repairs != null) reqById = _ticket.Repairs.RequestedById;

            if(reqById != null) employee = EmployeeProviderAccessor.Provider?.GetEmployeeById(reqById);
        }
    }
}
