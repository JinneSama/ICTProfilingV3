using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.ReportViewModel;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Base;
using ICTProfilingV3.Services.Employees;
using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ICTProfilingV3.Services
{
    public class TechSpecsService : BaseDataService<TechSpecs, int>, ITechSpecsService
    {
        private readonly IRepository<int, TicketRequest> _ticketRequestRepo;
        private readonly IRepository<int, TechSpecsICTSpecs> _tsICTSpecsRepo;
        private readonly IRepository<int, TechSpecsICTSpecsDetails> _tsICTSpecsDetailsRepo;
        private readonly IICTUserManager _userManager;
        private readonly UserStore _userStore;

        public IBaseDataService<TechSpecsBasis, int> TechSpecsBasisBaseService { get; set; }
        public IBaseDataService<TechSpecsBasisDetails, long> TechSpecsBasisDetailBaseService { get; set; }

        public TechSpecsService(IRepository<int, TechSpecs> baseRepo, IRepository<int, TicketRequest> ticketRequestRepo,
            UserStore userStore, IRepository<int, TechSpecsICTSpecsDetails> tsICTSpecsDetailsRepo, IBaseDataService<TechSpecsBasis, int> techSpecsBasisBaseService, 
            IBaseDataService<TechSpecsBasisDetails, long> techSpecsBasisDetailBaseService, IICTUserManager userManager,
            IRepository<int, TechSpecsICTSpecs> tsICTSpecsRepo) : base(baseRepo)
        {
            _ticketRequestRepo = ticketRequestRepo;
            _tsICTSpecsDetailsRepo = tsICTSpecsDetailsRepo;
            _userStore = userStore;
            TechSpecsBasisBaseService = techSpecsBasisBaseService;
            TechSpecsBasisDetailBaseService = techSpecsBasisDetailBaseService;
            _userManager = userManager;
            _tsICTSpecsRepo = tsICTSpecsRepo;
        }

        public override async Task DeleteAsync(int id)
        {
            _ticketRequestRepo.Delete(id);
            await _ticketRequestRepo.SaveChangesAsync();
            await base.DeleteAsync(id);
        }

        public override async Task<TechSpecs> AddAsync(TechSpecs entity)
        {
            var ticket = new TicketRequest()
            {
                DateCreated = DateTime.Now,
                TicketStatus = TicketStatus.Accepted,
                RequestType = RequestType.TechSpecs,
                CreatedBy = _userStore.UserId
            };
            await _ticketRequestRepo.AddAsync(ticket);

            entity.Id = ticket.Id;
            return await base.AddAsync(entity);
        }

        public async Task<TechSpecs> AddRepairTechSpecsAsync(TechSpecs techSpecs)
        {
            var ticket = new TicketRequest()
            {
                DateCreated = DateTime.Now,
                TicketStatus = TicketStatus.Accepted,
                RequestType = RequestType.TechSpecs,
                CreatedBy = _userStore.UserId,
                IsRepairTechSpecs = true
            };
            await _ticketRequestRepo.AddAsync(ticket);

            techSpecs.Id = ticket.Id;
            return await base.AddAsync(techSpecs);
        }

        public IEnumerable<TechSpecsViewModel> GetTSViewModels(bool isTechSpecs)
        {
            var res = base.GetAll().Where(x => x.TicketRequest.StaffId != null && x.TicketRequest.IsDeleted != true)
                .Include(x => x.TicketRequest)
                .Include(x => x.Repairs)
                .Include(x => x.TicketRequest.ITStaff.Users)
                .Include(x => x.TechSpecsICTSpecs.Select(s => s.EquipmentSpecs.Equipment))
                .OrderByDescending(x => x.DateRequested)
                .ToList();

            if (isTechSpecs) res = res.Where(x => x.TicketRequest.IsRepairTechSpecs != true).ToList();
            else res = res.Where(x => x.TicketRequest.IsRepairTechSpecs == true).ToList();
            var ts = res.Select(x => new TechSpecsViewModel
            {
                Id = x.Id,
                Status = x.TicketRequest.TicketStatus,
                DateAccepted = x.DateAccepted,
                DateRequested = x.DateRequested,
                Office = HRMISEmployees.GetEmployeeById(x.ReqById)?.Office,
                Division = HRMISEmployees.GetEmployeeById(x.ReqById)?.Division,
                TicketId = x.TicketRequest.Id,
                RepairId = x.Repairs.Count == 0 ? 0 : x.Repairs.FirstOrDefault().Id,
                TechSpecs = x,
                AssignedTo = x.TicketRequest.ITStaff.Users.FullName,
                Equipment = x.TechSpecsICTSpecs.Count == 0 ? "N/A" : string.Join(", ", x.TechSpecsICTSpecs?.Select(s => s?.EquipmentSpecs?.Equipment?.EquipmentName ?? ""))
            });
            return ts;
        }

        public async Task<TechSpecsReportViewModel> GetReportViewModel(int techSpecsId)
        {
            var ts = await base.GetByFilterAsync(x => x.Id == techSpecsId,
                x => x.Repairs,
                x => x.TechSpecsICTSpecs,
                x => x.TechSpecsICTSpecs.Select(s => s.TechSpecsICTSpecsDetails),
                x => x.TechSpecsICTSpecs.Select(s => s.EquipmentSpecs),
                x => x.TechSpecsICTSpecs.Select(s => s.EquipmentSpecs.Equipment));

            var chief = HRMISEmployees.GetEmployeeById(ts.ReqByChiefId);
            var staff = HRMISEmployees.GetEmployeeById(ts.ReqById);
            var data = new TechSpecsReportViewModel()
            {
                PrintedBy = _userStore.Username,
                DatePrinted = DateTime.Now,
                Office = string.Join(" ", staff?.Office, staff?.Division),
                Chief = chief.Employee,
                ChiefPosition = chief?.Position,
                Staff = staff?.Employee,
                StaffPosition = staff?.Position,
                TechSpecs = ts,
                PreparedBy = await _userManager.FindUserAsync(ts.PreparedById),
                ReviewedBy = await _userManager.FindUserAsync(ts.ReviewedById),
                NotedBy = await _userManager.FindUserAsync(ts.NotedById),
                RepairId = ts.Repairs.Count == 0 ? 0 : ts.Repairs.FirstOrDefault().Id
            };
            return data;
        }

        #region TechSpecsICTSpecs
        public async Task<TechSpecsICTSpecs> GetTSICTSpecsById(int id)
        {
            return await _tsICTSpecsRepo.GetById(id);
        }
        public IQueryable<TechSpecsICTSpecsViewModel> GetTSICTSpecs(int tsId)
        {
            var ts = _tsICTSpecsRepo.Fetch(x => x.TechSpecsId == tsId)
                .Include(x => x.EquipmentSpecs)
                .Include(x => x.EquipmentSpecs.Equipment).OrderBy(o => o.ItemNo);
            
            var tsICTViewModel = ts.Select(x => new TechSpecsICTSpecsViewModel
            {
                Id = x.Id,
                ItemNo = x.ItemNo,
                Quantity = x.Quantity,
                Unit = x.Unit,
                Equipment = x.EquipmentSpecs.Equipment.EquipmentName,
                EquipmentSpecsId = x.EquipmentSpecsId,
                Description = x.Description,
                UnitCost = x.UnitCost,
                TotalCost = x.TotalCost,
                Purpose = x.Purpose,
                TechSpecsICTSpecsDetails = x.TechSpecsICTSpecsDetails,
                TechSpecsId = x.TechSpecsId
            });

            return tsICTViewModel;
        }

        public async Task SaveTSICTSpecsUpdateAsync()
        {
            await _tsICTSpecsRepo.SaveChangesAsync();
        }

        public async Task AddTechSpecsICTSpecsAsync(TechSpecsICTSpecs techSpecsICTSpecs)
        {
            await _tsICTSpecsRepo.AddAsync(techSpecsICTSpecs);
            await _tsICTSpecsRepo.SaveChangesAsync();
        }

        public async Task DeleteTechSpecsICTSpecsById(int id)
        {
            _tsICTSpecsRepo.Delete(id);
            await _tsICTSpecsRepo.SaveChangesAsync();
        }
        #endregion

        #region TechSpecsICTSpecsDetails
        public IQueryable<TechSpecsICTSpecsDetails> GetTSICTSpecsDetails()
        {
            return _tsICTSpecsDetailsRepo.GetAll();
        }
        public async Task<TechSpecsICTSpecsDetails> GetTSICTSpecsDetailById(int Id)
        {
            return await _tsICTSpecsDetailsRepo.GetById(Id);
        }

        public async Task SaveTSICTSpecsDetailsAsync()
        {
            await _tsICTSpecsDetailsRepo.SaveChangesAsync();
        }

        public async Task AddTechSpecsICTSpecsDetailAsync(TechSpecsICTSpecsDetails techSpecsICTSpecs)
        {
            await _tsICTSpecsDetailsRepo.AddAsync(techSpecsICTSpecs);
        }

        public async Task DeleteTechSpecsICTSpecsDetailById(int id)
        {
            _tsICTSpecsDetailsRepo.Delete(id);
            await _tsICTSpecsDetailsRepo.SaveChangesAsync();
        }

        public async Task DeleteTechSpecsICTSpecsDetailRange(Expression<Func<TechSpecsICTSpecsDetails, bool>> expression)
        {
            _tsICTSpecsDetailsRepo.DeleteRange(expression);
            await _tsICTSpecsDetailsRepo.SaveChangesAsync();
        }
        #endregion
    }
}
