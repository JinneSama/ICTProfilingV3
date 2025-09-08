using ICTProfilingV3.DataTransferModels.ReportViewModel;
using Models.Entities;
using System.Threading.Tasks;

namespace ICTProfilingV3.Interfaces
{
    public interface IComparisonReportService : IBaseDataService<ComparisonReport, int>
    {
        IBaseDataService<ComparisonReportSpecs, int> ComparisonReportSpecsService { get; set; }
        IBaseDataService<ComparisonReportSpecsDetails, int> ComparisonReportSpecsDetailsService { get; set; }
        Task<ComparisonReportPrintViewModel> GetComparisonReportPrintModel(int deliveryId);
    }
}
