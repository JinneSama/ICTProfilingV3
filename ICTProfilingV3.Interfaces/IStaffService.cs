using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.DataTransferModels.ViewModels;
using Models.Entities;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ICTProfilingV3.Interfaces
{
    public interface IStaffService : IBaseDataService<ITStaff, int>
    {
        IQueryable<ITStaff> FetchStaff(Expression<Func<ITStaff, bool>> filter);
        IEnumerable<StaffViewModel> GetStaffDTM();
        Task<StaffModel> GetStaffModel(int ticketId);
        Task<Sections?> Section();
    }
}
