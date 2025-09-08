using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using Models.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using ICTProfilingV3.Core.Common;
using ICTProfilingV3.Services.Employees;
using ICTProfilingV3.Services.Base;

namespace ICTProfilingV3.Services
{
    public class TicketRequestService : BaseDataService<TicketRequest, int>, ITicketRequestService
    {
        private readonly IRepository<int, TicketRequest> _ticketRepository;
        private readonly IRepository<int, Deliveries> _deliveriesRepo;
        private readonly IRepository<int, Repairs> _repairRepo;
        private readonly IRepository<int, TechSpecs> _tsRepo;

        private readonly IICTUserManager _userManager;
        private readonly IICTRoleManager _roleManager;
        private readonly UserStore _userStore;

        public TicketRequestService(IRepository<int, TicketRequest> baseRepo, IRepository<int, TicketRequest> ticketRepository, IICTUserManager userManager,
            IICTRoleManager roleManager, UserStore userStore, IBaseDataService<TicketRequestStatus, int> ticketRequestStatusService,
            IRepository<int, Deliveries> deliveriesRepo, IRepository<int, Repairs> repairRepo, IRepository<int, TechSpecs> tsRepo) : base(baseRepo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _ticketRepository = ticketRepository;
            _userStore = userStore;
            TicketRequestStatusService = ticketRequestStatusService;
            _deliveriesRepo = deliveriesRepo;
            _repairRepo = repairRepo;
            _tsRepo = tsRepo;
        }
        public IBaseDataService<TicketRequestStatus, int> TicketRequestStatusService { get; set; }

        public async Task<bool> CanAssignTicket()
        {
            var user = await _userManager.FindUserAsync(_userStore.UserId);
            if (user.Roles == null) return false;
            var role = await _roleManager.GetRoleDesignations(user.Roles.FirstOrDefault().RoleId);
            if (role == null) return false;

            return role.Select(x => x.Designation).ToList().Contains(Designation.AssignTo);
        }

        public override async Task DeleteAsync(int id)
        {
            var ticket = await base.GetByIdAsync(id);
            if (ticket.RequestType == RequestType.Deliveries)
            {

            }
            if (ticket.RequestType == RequestType.TechSpecs)
            {

            }
            if (ticket.RequestType == RequestType.Repairs)
            {

            }
            await base.DeleteAsync(id);
        }

        #region GetTicketRequests
        public async Task<IEnumerable<TicketRequestDTM>> GetTicketRequests()
        {
            var ticketRequests = await _ticketRepository.GetAll().Where(x => x.IsDeleted == false || x.IsDeleted == null)
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
                Office = SetEmployee(s)?.Requestor?.Office + " " + SetEmployee(s)?.Requestor?.Division,
                RequestedBy = SetEmployee(s).Requestor?.Employee ?? "",
                DeliveredBy = SetEmployee(s)?.DeliveredBy?.Employee ?? "",
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
            {
                if(ticket?.Repairs?.PPEs?.PPEsSpecs == null)
                    return string.Empty;
                equipment = string.Join(",", ticket.Repairs.PPEs.PPEsSpecs.Select(x => $"{x.Quantity} {x?.Model?.Brand?.EquipmentSpecs?.Equipment?.EquipmentName}"));
            }
            return equipment;
        }

        private EmployeeInfoDTM SetEmployee(TicketRequest ticket)
        {
            var employeeInfo = new EmployeeInfoDTM();
            long? reqById = null;
            long? reqyByChief = null;
            long? deliveredBy = null;
            if (ticket.RequestType == RequestType.Deliveries)
            {
                reqById = ticket.Deliveries.RequestedById;
                reqyByChief = ticket.Deliveries.ReqByChiefId;
                deliveredBy = ticket.Deliveries.DeliveredById;
            }
            if (ticket.RequestType == RequestType.TechSpecs)
            {
                reqById = ticket.TechSpecs.ReqById;
                reqyByChief = ticket.TechSpecs.ReqByChiefId;
            }
            if (ticket.RequestType == RequestType.Repairs)
            {
                reqById = ticket?.Repairs?.RequestedById;
                reqyByChief = ticket?.Repairs?.ReqByChiefId;
                deliveredBy = ticket?.Repairs?.DeliveredById;
            }

            if (reqById != null)
            {
                employeeInfo.Requestor = HRMISEmployees.GetEmployeeById(reqById);
                employeeInfo.Chief = HRMISEmployees.GetEmployeeById(reqyByChief);
                employeeInfo.DeliveredBy = HRMISEmployees.GetEmployeeById(deliveredBy);
            };

            return employeeInfo;
        }
        #endregion
    }
}
