using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using Models.HRMISEntites;
using Models.ViewModels;
using Models.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ICTProfilingV3.Services
{
    public class TicketRequestService : ITicketRequestService
    {
        private readonly IRepository<int, TicketRequest> _ticketRepository;
        private EmployeesViewModel _employee;
        private EmployeesViewModel _chief;
        public TicketRequestService(IRepository<int, TicketRequest> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public async Task<IEnumerable<TicketRequestDTM>> GetTicketRequests()
        {
            var ticketRequests = await _ticketRepository.GetAll()
            .Include(t => t.ITStaff.Users)
            .Include(t => t.Deliveries)
            .Include(t => t.Deliveries.Supplier)
            .Include(t => t.Deliveries.DeliveriesSpecs.Select(d => d.Model.Brand.EquipmentSpecs.Equipment))
            .Include(t => t.TechSpecs)
            .Include(t => t.TechSpecs.TechSpecsICTSpecs.Select(d => d.EquipmentSpecs.Equipment))
            .Include(t => t.Repairs)
            .Include(t => t.Repairs.PPEs.PPEsSpecs.Select(p => p.Model.Brand.EquipmentSpecs.Equipment))
            .Where(w => w.IsRepairTechSpecs != true).OrderBy(x => x.DateCreated)
            .ToListAsync();
            
            return ticketRequests.Select(s => new TicketRequestDTM
            {
                Id = s.Id,
                DateRequested = s.DateCreated,
                Status = EnumHelper.GetEnumDescription(s?.TicketStatus ?? TicketStatus.Accepted),
                EnumStatus = s.TicketStatus,
                AssignedTo = s?.ITStaff?.Users?.OFMISUsername ?? "",
                TypeOfRequest = s.RequestType,
                Equipments = Equipment(s),
                Office = SetEmployee(s).Requestor.Office + " " + SetEmployee(s).Requestor.Division,
                RequestedBy = SetEmployee(s).Requestor?.Employee ?? "",
                DeliveredBy = SetEmployee(s).Chief?.Employee ?? "",
                Supplier = s?.Deliveries?.Supplier?.SupplierName ?? ""
            });
        }
        private string Equipment(TicketRequest ticket)
        {
            string equipment = string.Empty;
            if (ticket.RequestType == RequestType.Deliveries)
                equipment = string.Join(",", ticket.Deliveries.DeliveriesSpecs.Select(x => $"{x.Quantity} {x?.Model?.Brand?.EquipmentSpecs?.Equipment?.EquipmentName}"));
            if (ticket.RequestType == RequestType.TechSpecs)
                equipment = string.Join(",", ticket.TechSpecs.TechSpecsICTSpecs.Select(x => $"{x.Quantity} {x?.EquipmentSpecs?.Equipment?.EquipmentName}"));
            if (ticket.RequestType == RequestType.Repairs)
                equipment = string.Join(",", ticket.Repairs.PPEs.PPEsSpecs.Select(x => $"{x.Quantity} {x?.Model?.Brand?.EquipmentSpecs?.Equipment?.EquipmentName}"));
            return equipment;
        }

        private EmployeeInfoDTM SetEmployee(TicketRequest ticket)
        {
            var employeeInfo = new EmployeeInfoDTM();
            long? reqById = null;
            long? reqyByChief = null;
            if (ticket.RequestType == RequestType.Deliveries)
            {
                reqById = ticket.Deliveries.RequestedById;
                reqyByChief = ticket.Deliveries.ReqByChiefId;
            }
            if (ticket.RequestType == RequestType.TechSpecs)
            {
                reqById = ticket.TechSpecs.ReqById;
                reqyByChief = ticket.TechSpecs.ReqByChiefId;
            }
            if (ticket.RequestType == RequestType.Repairs)
            {
                reqById = ticket.Repairs.RequestedById;
                reqyByChief = ticket.Repairs.ReqByChiefId;
            }

            if (reqById != null)
            {
                employeeInfo.Requestor = HRMISEmployees.GetEmployeeById(reqById);
                employeeInfo.Chief = HRMISEmployees.GetEmployeeById(reqyByChief);
            };

            return employeeInfo;
        }
    }
}
