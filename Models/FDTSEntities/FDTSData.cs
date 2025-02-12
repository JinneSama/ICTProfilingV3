using Models.Service.DTOModels;
using Models.Service;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Models.FDTSEntities
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
