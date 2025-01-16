using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Models.OFMISEntities
{
    public class OFMISUsers
    {
        private static OFMISEntities ofmis;
        private static IQueryable<User> users;
        public static void InitUsers()
        {
            ofmis = new OFMISEntities();
            users = ofmis.Users.AsQueryable();
        }

        public static async Task<User> GetUser(string username)
        {
            var loggedUser = await users.FirstOrDefaultAsync(x => x.UserName == username);
            if(loggedUser == null) return null;
            return loggedUser;
        }
    }
}
