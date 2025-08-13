using ICTProfilingV3.DataTransferModels.ServiceModels.DTOModels;
using System.Threading.Tasks;

namespace ICTProfilingV3.Services.Employees
{
    public class OFMISUsers
    {
        private static OFMISService _service;
        public static void InitUsers(OFMISService service)
        {
            _service = service;
        }

        public static async Task<OFMISUsersDto> GetUser(string username)
        {
            var loggedUser = await _service.GetUser(username);
            if(loggedUser == null) return null;
            return loggedUser;
        }
    }
}
