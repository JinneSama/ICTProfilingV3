using Models.Entities;
using Models.Enums;
using System.Threading.Tasks;

namespace ICTProfilingV3.Interfaces
{
    public interface IEvaluationService : IBaseDataService<EvaluationSheet, int>
    {
        Task CreateEvaluationSheet(RequestType requestType, int sheetParentId);
    }
}
