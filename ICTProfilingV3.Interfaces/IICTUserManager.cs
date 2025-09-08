using System.Collections.Generic;
using System.Threading.Tasks;
using ICTProfilingV3.DataTransferModels.Models;
using Microsoft.AspNet.Identity;
using Models.Entities;

namespace ICTProfilingV3.Interfaces
{
    public interface IICTUserManager
    {
        Task<Users> FindUserByUsername(string username);
        Task<Users> FindUserAsync(string userId);
        Task<(IdentityResult result, string userId)> CreateUser(Users user , string password);
        Task UpdateUser(UserModel users);
        Task<(bool success, Users user)> Login(string username, string password);
        void DeleteUser(string UserId);
        IEnumerable<Users> GetUsers();
    }
}