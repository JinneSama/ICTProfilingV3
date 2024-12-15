using EntityManager.Context;
using Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using Models.Managers;

namespace EntityManager.Managers
{
    public class UserManager : UserManager<Users>
    {
        public UserManager(IUserStore<Users> store) : base(store)
        {
        }

        public static UserManager Create()
        {
            var store = new UserStore<Users>(new ApplicationDbContext());
            var manager = new UserManager(store);

            manager.UserValidator = new UserValidator<Users>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            manager.UserTokenProvider = new CustomTokenProvider<Users>();

            return manager;
        }
    }
}