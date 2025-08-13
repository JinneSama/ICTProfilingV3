using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System;

namespace ICTProfilingV3.Core.Token
{
    public class CustomTokenProvider<TUser> : IUserTokenProvider<TUser, string>
    where TUser : class, IUser<string>
    {
        public Task<string> GenerateAsync(string purpose, UserManager<TUser, string> manager, TUser user)
        {
            var token = Guid.NewGuid().ToString();
            return Task.FromResult(token);
        }

        public Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser, string> manager, TUser user)
        {
            return Task.FromResult(true);
        }

        public Task NotifyAsync(string token, UserManager<TUser, string> manager, TUser user)
        {
            Console.WriteLine($"Token for user {user.UserName}: {token}");
            return Task.CompletedTask;
        }

        public Task<bool> IsValidProviderForUserAsync(UserManager<TUser, string> manager, TUser user)
        {
            return Task.FromResult(true);
        }
    }
}
