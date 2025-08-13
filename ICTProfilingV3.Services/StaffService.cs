using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.DataTransferModels.ViewModels;
using ICTProfilingV3.Interfaces;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ICTProfilingV3.Services
{
    public class StaffService : IStaffService
    {
        private readonly IRepository<int, ITStaff> _staffRepository;
        public StaffService(IRepository<int, ITStaff> staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public IQueryable<ITStaff> FetchStaff(Expression<Func<ITStaff, bool>> filter)
        {
            return _staffRepository.Fetch(filter).Include(i => i.Users);
        }

        public IQueryable<ITStaff> GetAllStaff()
        {
            return _staffRepository.GetAll().Include(i => i.Users);
        }

        public IEnumerable<StaffViewModel> GetStaffDTM()
        {
            var staffList = _staffRepository.Fetch(x => x.Users.IsDeleted == false)
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
    }
}
