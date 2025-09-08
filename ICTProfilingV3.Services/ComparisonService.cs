using ICTProfilingV3.Interfaces;
using ICTProfilingV3.Services.Base;
using Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ICTProfilingV3.Services
{
    public class ComparisonService : BaseDataService<ComparisonReportFiles, int>, IComparisonService
    {
        public ComparisonService(IRepository<int, ComparisonReportFiles> baseRepo) : base(baseRepo)
        {
        }

        public override async Task<ComparisonReportFiles> AddAsync(ComparisonReportFiles entity)
        {
            entity.Version = GetLatestVersion(entity.DeliveriesId) + 1;
            entity.FileName = $"ComparisonReport_{entity.DeliveriesId}_{entity.Version}.xlsx";
            return await base.AddAsync(entity);
        }
        private int GetLatestVersion(int deliveriesId)
        {
            var last = GetAll().Where(x => x.DeliveriesId == deliveriesId)
                               .OrderByDescending(x => x.Version)
                               .FirstOrDefault();

            if (last == null)
                return 0;

            return last.Version;
        }
    }
}
