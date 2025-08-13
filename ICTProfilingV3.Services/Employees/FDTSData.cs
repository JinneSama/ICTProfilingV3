using ICTProfilingV3.DataTransferModels.ServiceModels.DTOModels;
using System.Threading.Tasks;

namespace ICTProfilingV3.Services.Employees
{
    public class FDTSData
    {
        private static FDTSService service;
        public static void InitData()
        {
            service = new FDTSService();
        }

        public static async Task<FDTSPRDetailsDto> GetData(string controlNo)
        {
            var details = await service.GetDetails(controlNo);
            if (details == null) return null;
            return details;
        }
    }
}
