using Models.Service;
using Models.Service.DTOModels;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Models.OFMISEntities
{
    public class OFMISUsers
    {
        private static OFMISService service;
        public static void InitUsers()
        {
           service = new OFMISService();
        }

        public static async Task<OFMISUsersDto> GetUser(string username)
        {
            var loggedUser = await service.GetUser(username);
            if(loggedUser == null) return null;
            return loggedUser;
        }
    }
}
