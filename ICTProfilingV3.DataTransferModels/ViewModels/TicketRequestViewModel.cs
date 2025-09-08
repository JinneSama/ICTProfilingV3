using Models.Entities;
using Models.Enums;
using Models.HRMISEntites;
using System.Linq;

namespace Models.ViewModels
{
    public class TicketRequestViewModel
    {
        private EmployeesViewModel employee;
        private EmployeesViewModel chief;
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
            long? reqyByChief = null;
            if (ticket.Deliveries != null)
            {
                reqById = ticket.Deliveries.RequestedById;
                reqyByChief = ticket.Deliveries.ReqByChiefId;
            }
            if (ticket.TechSpecs != null)
            {
                reqById = ticket.TechSpecs.ReqById;
                reqyByChief = ticket.TechSpecs.ReqByChiefId;
            }
            if (ticket.Repairs != null)
            {
                reqById = ticket.Repairs.RequestedById;
                reqyByChief = ticket.Repairs.ReqByChiefId;
            }

            if (reqById != null)
            {
                employee = HRMISEmployees.GetEmployeeById(reqById);
                chief = HRMISEmployees.GetEmployeeById(reqyByChief);
            };
        }
        private string Equipment()
        {
            string equipment = string.Empty;
            if (_ticketRequest.RequestType == Enums.RequestType.Deliveries)
                equipment = string.Join(",", _ticketRequest.Deliveries.DeliveriesSpecs.Select(x => $"{x.Quantity} {x?.Model?.Brand?.EquipmentSpecs?.Equipment?.EquipmentName}"));
            if (_ticketRequest.RequestType == Enums.RequestType.TechSpecs)
                equipment = string.Join(",", _ticketRequest.TechSpecs.TechSpecsICTSpecs.Select(x => $"{x.Quantity} {x?.EquipmentSpecs?.Equipment?.EquipmentName}"));
            if (_ticketRequest.RequestType == Enums.RequestType.Repairs)
                equipment = string.Join(",", _ticketRequest.Repairs.PPEs.PPEsSpecs.Select(x => $"{x.Quantity} {x?.Model?.Brand?.EquipmentSpecs?.Equipment?.EquipmentName}"));
            return equipment;
        }
        public string Equipments => Equipment();
        public string Supplier => GetSupplier();

        private string GetSupplier()
        {
            return TicketRequest?.Deliveries?.Supplier?.SupplierName ?? "";
        }

        public string DeliveredBy => GetDeliveredBy();
        private string GetDeliveredBy()
        {
            long? reqById = null;
            if (TicketRequest.RequestType == RequestType.TechSpecs)
                reqById = TicketRequest.TechSpecs?.ReqById ?? null;
            if (TicketRequest.RequestType == RequestType.Deliveries)
                reqById = TicketRequest.Deliveries?.DeliveredById ?? null;
            if (TicketRequest.RequestType == RequestType.Repairs)
                reqById = TicketRequest.Repairs?.DeliveredById ?? null;

            if (reqById != null)
                return HRMISEmployees.GetEmployeeById(reqById).Employee;
            else
                return "";
        }
    }
}
