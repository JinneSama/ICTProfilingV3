using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Models.Entities;
using Models.Models;
using Models.ViewModels;

namespace EntityManager.Managers.User
{
    public interface IICTUserManager
    {
        Task<Users> FindUserAsync(string userId);
        Task<(IdentityResult result, string userId)> CreateUser(Users user , string password);
        Task UpdateUser(UserModel users);
        Task<(bool success, Users user)> Login(string username, string password);
        void DeleteUser(string UserId);
        IEnumerable<Users> GetUsers();
    }
}