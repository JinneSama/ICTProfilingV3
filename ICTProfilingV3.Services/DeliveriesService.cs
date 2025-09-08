using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Base;
using ICTProfilingV3.Services.Employees;
using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.Services
{
    public class DeliveriesService : BaseDataService<Deliveries, int>, IDeliveriesService
    {
        private readonly IRepository<int, TicketRequest> _ticketRequestRepo;
        private readonly IRepository<int, DeliveriesSpecs> _deliveriesSpecsRepo;
        private readonly UserStore _userStore;
        public DeliveriesService(IRepository<int, Deliveries> baseRepo, IRepository<int, TicketRequest> ticketRequestRepo,
            UserStore userStore, IRepository<int, DeliveriesSpecs> deliveriesSpecsRepo,
            IBaseDataService<DeliveriesSpecs, int> deliveriesSpecsBaseService,
            IBaseDataService<DeliveriesSpecsDetails, long> deliveriesSpecsDetailsBaseService) : base(baseRepo)
        {
            _ticketRequestRepo = ticketRequestRepo;
            _userStore = userStore;
            _deliveriesSpecsRepo = deliveriesSpecsRepo;
            DeliveriesSpecsDetailsBaseService = deliveriesSpecsDetailsBaseService;
            DeliveriesSpecsBaseService = deliveriesSpecsBaseService;
        }

        public IBaseDataService<DeliveriesSpecs, int> DeliveriesSpecsBaseService { get; set; }
        public IBaseDataService<DeliveriesSpecsDetails, long> DeliveriesSpecsDetailsBaseService { get; set; }

        public override async Task<Deliveries> AddAsync(Deliveries entity)
        {
            var ticket = new TicketRequest()
            {
                DateCreated = DateTime.Now,
                TicketStatus = TicketStatus.Accepted,
                RequestType = RequestType.Deliveries,
                CreatedBy = _userStore.UserId
            };

            await _ticketRequestRepo.AddAsync(ticket);
            entity.Id = ticket.Id;
            return await base.AddAsync(entity);
        }

        public override async Task DeleteAsync(int id)
        {
            _ticketRequestRepo.Delete(id);
            await _ticketRequestRepo.SaveChangesAsync();

            _deliveriesSpecsRepo.DeleteRange(x => x.DeliveriesId == id);
            await _deliveriesSpecsRepo.SaveChangesAsync();

            await base.DeleteAsync(id);
        }

        public DeliveriesDetailsViewModel GetDeliveriesDetailViewModels(DeliveriesViewModel model)
        {
            var deliveriesDetails = model.Deliveries;
            var requestingEmployee = HRMISEmployees.GetEmployeeById(deliveriesDetails.RequestedById);
            var chiefID = HRMISEmployees.GetChief(requestingEmployee.Office, requestingEmployee.Division, deliveriesDetails.ReqByChiefId)?.ChiefId;
            var delDetailsModel = new DeliveriesDetailsViewModel();
            delDetailsModel.Chief = HRMISEmployees.GetEmployeeById(chiefID)?.Employee;
            delDetailsModel.EpisNo = deliveriesDetails.Id.ToString();
            delDetailsModel.Office = string.Join(" ", requestingEmployee?.Office, requestingEmployee?.Division);
            delDetailsModel.ReqBy = requestingEmployee?.Employee;
            delDetailsModel.Tel = deliveriesDetails.ContactNo;
            delDetailsModel.DeliveredBy = HRMISEmployees.GetEmployeeById(deliveriesDetails.DeliveredById)?.Employee;
            delDetailsModel.SupplierName = deliveriesDetails.Supplier?.SupplierName;
            delDetailsModel.SupplierAddress = deliveriesDetails.Supplier?.Address;
            delDetailsModel.SupplierTelNo = deliveriesDetails.Supplier?.TelNumber;
            delDetailsModel.DeliveredDate = deliveriesDetails.DeliveredDate ?? DateTime.MinValue;
            
            return delDetailsModel;
        }

        public async Task<IEnumerable<DeliveriesViewModel>> GetDeliveriesViewModels()
        {
            var deliveries = await base.GetAll().Where(x => x.TicketRequest.ITStaff != null && x.TicketRequest.IsDeleted != true)
                .Include(x => x.Supplier)
                .Include(x => x.TicketRequest)
                .Include(x => x.TicketRequest.ITStaff)
                .Include(x => x.TicketRequest.ITStaff.Users)
                .Include(x => x.DeliveriesSpecs)
                .Include(x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment))
                .OrderByDescending(x => x.DateRequested)
                .ToListAsync();

            var delData = deliveries.Select(x => new DeliveriesViewModel
            {
                Id = x.Id,
                Status = x.TicketRequest.TicketStatus,
                TicketNo = x.TicketRequest.Id,
                Office = HRMISEmployees.GetEmployeeById(x.RequestedById)?.Office,
                Supplier = x.Supplier.SupplierName,
                DeliveryId = "EPiS-" + x.Id,
                PONo = x.PONo,
                Deliveries = x,
                DateCreated = x.DateRequested.Value,
                AssignedTo = x.TicketRequest.ITStaff?.Users?.FullName,
                Equipment = x.DeliveriesSpecs.Count() > 0 ? string.Join(", ", x.DeliveriesSpecs?.Select(s => s.Model?.Brand?.EquipmentSpecs?.Equipment?.EquipmentName ?? "")) : "N/A",
            });
            return delData;
        }
    }
}
