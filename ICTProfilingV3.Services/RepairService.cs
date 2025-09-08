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
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.Services
{
    public class RepairService : BaseDataService<Repairs, int>, IRepairService
    {
        private readonly ITicketRequestService _ticketService;
        private readonly IRepository<int, PPEs> _ppeRepo;
        private readonly UserStore _userStore;

        public RepairService(IRepository<int, Repairs> baseRepo, IRepository<int, PPEs> ppeRepo,
            UserStore userStore, ITicketRequestService ticketService) : base(baseRepo)
        {
            _ppeRepo = ppeRepo;
            _userStore = userStore;
            _ticketService = ticketService;
        }

        public override async Task<Repairs> AddAsync(Repairs entity)
        {
            var ticket = new TicketRequest()
            {
                DateCreated = DateTime.Now,
                TicketStatus = TicketStatus.Accepted,
                RequestType = RequestType.Repairs,
                CreatedBy = _userStore.UserId
            };
            var newTicket = await _ticketService.AddAsync(ticket);

            var repairs = new Repairs()
            {
                Id = newTicket.Id
            };

            return await base.AddAsync(repairs);
        }

        public override IQueryable<Repairs> GetAll()
        {
            return base.GetAll().Where(x => x.TicketRequest.IsDeleted != true);
        }

        public override async Task DeleteAsync(int id)
        {
            await _ticketService.DeleteAsync(id);
            await base.DeleteAsync(id);
        }
        public async Task<PPEs> GetPPE(int PPEId)
        {
            return await _ppeRepo.GetById(PPEId);
        }

        public IEnumerable<PPEsViewModel> GetRepairPPE()
        {
            var ppes = _ppeRepo.GetAll().ToList();
            var ppeViewModels = ppes.Select(x => new PPEsViewModel
            {
                Id = x.Id,
                PropertyNo = x.PropertyNo,
                IssuedTo = HRMISEmployees.GetEmployeeById(x.IssuedToId)?.Employee,
                DateCreated = x.DateCreated,
                Office = HRMISEmployees.GetEmployeeById(x.IssuedToId)?.Office,
                Status = x?.Status
            });
            return ppeViewModels;
        }

        public IEnumerable<RepairViewModel> GetRepairViewModels()
        {
            var res = base.GetAll().Where(x => x.TicketRequest.StaffId != null)
                .Include(x => x.TicketRequest)
                .Include(x => x.PPEs)
                .Include(x => x.PPEs.PPEsSpecs.Select(s => s.Model.Brand.EquipmentSpecs.Equipment))
                .OrderByDescending(x => x.DateCreated).ToList();

            var repair = res.Select(x => new RepairViewModel
            {
                Id = x.Id,
                Status = x.TicketRequest.TicketStatus,
                DateCreated = x.DateCreated,
                PropertyNo = x.PPEs?.PropertyNo,
                IssuedTo = HRMISEmployees.GetEmployeeById(x.RequestedById)?.Employee,
                Office = HRMISEmployees.GetEmployeeById(x.RequestedById)?.Office,
                RepairId = "EPiS-" + x.Id,
                Repair = x,
                AssignedTo = x.TicketRequest.StaffId,
                Equipment = x.PPEs?.PPEsSpecs?.FirstOrDefault()?.Model?.Brand?.EquipmentSpecs?.Equipment?.EquipmentName ?? ""
            });
            return repair;
        }
    }
}
