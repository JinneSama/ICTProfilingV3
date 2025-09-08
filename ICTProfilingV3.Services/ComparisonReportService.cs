using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.ReportViewModel;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Base;
using ICTProfilingV3.Services.Employees;
using Models.Entities;
using System;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.Services
{
    public class ComparisonReportService : BaseDataService<ComparisonReport, int>, IComparisonReportService
    {
        private readonly IRepository<int, Deliveries> _deliveriesRepo;
        private readonly IRepository<int, ComparisonReport> _comparisonReportRepo;
        private readonly UserStore _userStore;
        public ComparisonReportService(IRepository<int, ComparisonReport> baseRepo, IRepository<int, Deliveries> deliveriesRepo, 
            IRepository<int, ComparisonReport> comparisonReportRepo, UserStore userStore,
            IBaseDataService<ComparisonReportSpecs, int> comparisonReportSpecsService,
            IBaseDataService<ComparisonReportSpecsDetails, int> comparisonReportSpecsDetailsService) : base(baseRepo)
        {
            _deliveriesRepo = deliveriesRepo;
            _comparisonReportRepo = comparisonReportRepo;
            _userStore = userStore;
            ComparisonReportSpecsService = comparisonReportSpecsService;
            ComparisonReportSpecsDetailsService = comparisonReportSpecsDetailsService;
        }

        public IBaseDataService<ComparisonReportSpecs, int> ComparisonReportSpecsService { get; set; }

        public IBaseDataService<ComparisonReportSpecsDetails, int> ComparisonReportSpecsDetailsService { get; set; }
        public override async Task<ComparisonReport> AddAsync(ComparisonReport entity)
        {
            //return base.AddAsync(entity);
            var deliveries = await _deliveriesRepo.GetByFilter(x => x.Id == entity.DeliveryId,
                x => x.Supplier,
                x => x.TicketRequest,
                x => x.TicketRequest.ITStaff,
                x => x.TicketRequest.ITStaff.Users,
                x => x.DeliveriesSpecs.Select(s => s.Model),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment));

            var cr = await base.GetByFilterAsync(x => x.DeliveryId == entity.DeliveryId);
            if (cr == null)
            {
                cr = new ComparisonReport { DeliveryId = entity.DeliveryId };
                await base.AddAsync(cr);
            }
            foreach (var item in deliveries.DeliveriesSpecs)
            {
                var crSpecs = new ComparisonReportSpecs
                {
                    ComparisonReportId = cr.Id,
                    ItemNo = item.ItemNo,
                    Quantity = item.Quantity,
                    Unit = item.Unit,
                    Type = item.Model?.Brand?.EquipmentSpecs?.Equipment?.EquipmentName,
                    ActualDelivery = item.Model.Brand.BrandName + " " + item.Model.ModelName
                };
                await ComparisonReportSpecsService.AddAsync(crSpecs);
                foreach (var itemDetails in item.DeliveriesSpecsDetails)
                {
                    var crSpecsDetails = new ComparisonReportSpecsDetails
                    {
                        ComparisonReportSpecs = crSpecs,
                        Type = itemDetails.Specs,
                        ActualDelivery = itemDetails.Description,
                        ItemOrder = itemDetails.ItemNo
                    };
                    await ComparisonReportSpecsDetailsService.AddAsync(crSpecsDetails);
                }
            }
            return cr;
        }

        public async Task<ComparisonReportPrintViewModel> GetComparisonReportPrintModel(int deliveryId)
        {
            var deliveries = await _deliveriesRepo.GetByFilter(x => x.Id == deliveryId,
                x => x.TicketRequest,
                x => x.Supplier,
                x => x.DeliveriesSpecs.Select(s => s.Model),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs),
                x => x.DeliveriesSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment));

            var cr = await _comparisonReportRepo.GetByFilter(x => x.DeliveryId == deliveries.Id,
                x => x.PreparedByUser,
                x => x.NotedByUser,
                x => x.ReviewedByUser,
                x => x.ComparisonReportSpecs,
                x => x.ComparisonReportSpecs.Select(s => s.ComparisonReportSpecsDetails));

            var employee = HRMISEmployees.GetEmployeeById(deliveries.RequestedById);
            var inspectActions = deliveries?.Actions?.Where(x => x.SubActivityId == 1138 || x.SubActivityId == 1139).OrderBy(x => x.ActionDate);

            var comparisonModel = new ComparisonReportPrintViewModel
            {
                DateOfDelivery = deliveries.DeliveredDate,
                RequestingOffice = employee.Office + " " + employee.Division,
                Supplier = deliveries.Supplier.SupplierName,
                Amount = (double)deliveries.DeliveriesSpecs.Sum(x => (x.UnitCost * x.Quantity)),
                EpisNo = "EPiS-" + deliveries.TicketRequest.Id.ToString(),
                TechInspectedDate = (DateTime)inspectActions?.FirstOrDefault()?.ActionDate,
                ComparisonReportSpecs = cr?.ComparisonReportSpecs,
                PreparedBy = cr?.PreparedByUser,
                ReviewedBy = cr?.ReviewedByUser,
                NotedBy = cr?.NotedByUser,
                DatePrinted = DateTime.Now,
                PrintedBy = _userStore.Username
            };
            return comparisonModel;
        }
    }
}
