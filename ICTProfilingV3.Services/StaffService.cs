using ICTProfilingV3.Core.Common;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Base;
using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ICTProfilingV3.Services
{
    public class StaffService : BaseDataService<ITStaff, int>, IStaffService
    {
        private readonly IRepository<int, TicketRequest> _ticketRequestsRepo;
        private readonly UserStore _userStore;
        public StaffService(IRepository<int, ITStaff> baseRepo, IRepository<int, TicketRequest> ticketRequestsRepo,
            UserStore userStore) : base(baseRepo)
        {
            _ticketRequestsRepo = ticketRequestsRepo;
            _userStore = userStore;
        }

        public IQueryable<ITStaff> FetchStaff(Expression<Func<ITStaff, bool>> filter)
        {
            return _baseRepo.Fetch(filter).Include(i => i.Users);
        }

        public override IQueryable<ITStaff> GetAll()
        {
            return base.GetAll().Include(i => i.Users); ;
        }

        public IEnumerable<StaffViewModel> GetStaffDTM()
        {
            var staffList = _baseRepo.Fetch(x => x.Users.IsDeleted == false)
                .Include(x => x.Users)
                .Include(x => x.TicketRequests)
                .OrderBy(x => x.Users.FullName)
                .ToList()
                .Select(x => new StaffViewModel 
                { 
                    Staff = x,
                    UserId = x.UserId
                });
            return staffList;
        }

        public async Task<StaffModel> GetStaffModel(int ticketId)
        {
            var ticket = await _ticketRequestsRepo.GetById(ticketId);
            var staff = await base.GetByFilterAsync(x => x.Id == ticket.StaffId, x => x.Users);
            
            var res = new StaffModel
            {
                AssignedTo =  staff?.Users?.UserName ?? "N / A",
                FullName = staff?.Users?.FullName ?? "N / A",
                InitialsVisible = true
            };
            return res;
        }

        public async Task<Sections?> Section()
        {
            var res = await GetStaff();
            return res?.Section;
        }
        private async Task<ITStaff> GetStaff()
        {
            var res = await base.GetByFilterAsync(x => x.UserId == _userStore.UserId);
            if (res == null) return null;
            return res;
        }
    }
}
