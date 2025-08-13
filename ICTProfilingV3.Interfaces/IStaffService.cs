using ICTProfilingV3.DataTransferModels;
using ICTProfilingV3.DataTransferModels.ViewModels;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ICTProfilingV3.Interfaces
{
    public interface IStaffService
    {
        IQueryable<ITStaff> GetAllStaff();
        IQueryable<ITStaff> FetchStaff(Expression<Func<ITStaff, bool>> filter);
        IEnumerable<StaffViewModel> GetStaffDTM();
    }
}
