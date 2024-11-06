using EntityManager.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EntityManager.Managers
{
    public class RoleManager : RoleManager<IdentityRole>
    {
        public RoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }

        public static RoleManager Create()
        {
            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            return new RoleManager(roleStore);
        }
    }
}