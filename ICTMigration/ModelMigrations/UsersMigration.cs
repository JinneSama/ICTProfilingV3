using EntityManager.Managers.User;
using ICTMigration.ICTv2Models;
using Models.Entities;
using Models.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace ICTMigration.ModelMigrations
{
    public class UsersMigration
    {
        private readonly ICTv2Entities iCTv2Entities;
        private readonly IUnitOfWork unitOfWork;
        private readonly IICTUserManager userManager;
        public UsersMigration()
        {
            iCTv2Entities = new ICTv2Entities();
            unitOfWork = new UnitOfWork();
            userManager = new ICTUserManager();
        }

        public async Task MigrateUsers()
        {
            var ictv2Users = iCTv2Entities.Users.ToList();
            foreach(var ictUser  in ictv2Users)
            {
                var user = new Users
                {
                    UserName = ictUser.UserName,
                    FullName = ictUser.FirstName + " " + ictUser.MiddleName + " " + ictUser.LastName,
                    Position = ictUser.Position,
                    Email = ictUser.UserName + "@gmail.com",
                    OFMISUsername = ictUser.OFMISUsername
                };
                await userManager.CreateUser(user, "Sample@123");
            };
        }
    }
}
