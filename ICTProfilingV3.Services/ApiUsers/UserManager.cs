using EntityManager.Context;
using Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using ICTProfilingV3.Core.Token;

namespace ICTProfilingV3.Services.ApiUsers
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