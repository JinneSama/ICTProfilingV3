using ICTProfilingV3.DataTransferModels.ReportViewModel;
using ICTProfilingV3.DataTransferModels.ViewModels;
using Models.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ICTProfilingV3.Interfaces
{
    public interface ITechSpecsService : IBaseDataService<TechSpecs, int>
    {
        IBaseDataService<TechSpecsBasis, int> TechSpecsBasisBaseService { get; set; }
        IBaseDataService<TechSpecsBasisDetails, long> TechSpecsBasisDetailBaseService { get; set; }
        Task<TechSpecs> AddRepairTechSpecsAsync(TechSpecs techSpecs);
        Task<TechSpecsICTSpecs> GetTSICTSpecsById(int id);
        IQueryable<TechSpecsICTSpecsViewModel> GetTSICTSpecs(int tsId);
        Task AddTechSpecsICTSpecsAsync(TechSpecsICTSpecs techSpecsICTSpecs);
        Task DeleteTechSpecsICTSpecsById(int id);
        Task SaveTSICTSpecsUpdateAsync();

        IQueryable<TechSpecsICTSpecsDetails> GetTSICTSpecsDetails();
        Task<TechSpecsICTSpecsDetails> GetTSICTSpecsDetailById(int Id);
        Task AddTechSpecsICTSpecsDetailAsync(TechSpecsICTSpecsDetails techSpecsICTSpecs);
        Task DeleteTechSpecsICTSpecsDetailById(int id);
        Task DeleteTechSpecsICTSpecsDetailRange(Expression<Func<TechSpecsICTSpecsDetails, bool>> expression);
        Task SaveTSICTSpecsDetailsAsync();
        IEnumerable<TechSpecsViewModel> GetTSViewModels(bool isTechSpecs);
        Task<TechSpecsReportViewModel> GetReportViewModel(int techSpecsId);
    }
}
