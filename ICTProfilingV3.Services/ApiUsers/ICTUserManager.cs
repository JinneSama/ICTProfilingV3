using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ICTProfilingV3.DataTransferModels.Models;
using ICTProfilingV3.Interfaces;
using Microsoft.AspNet.Identity;
using Models.Entities;
using Models.Models;

namespace ICTProfilingV3.Services.ApiUsers
{
    public class ICTUserManager : IICTUserManager, IDisposable
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private bool disposed;

        public ICTUserManager()
        {
            _userManager = UserManager.Create();
            _roleManager = RoleManager.Create();
        }

        public async Task<(IdentityResult result, string userId)> CreateUser(Users user, string password)
        {
            var res = await _userManager.CreateAsync(user , password);
            return (res,user.Id);
        }

        public async Task UpdateUser(UserModel userModel)
        {
            var user = await _userManager.FindByIdAsync(userModel.UserId);
            if (user == null) return;

            user.FullName = userModel.Fullname;
            user.Position = userModel.Position;
            user.UserName = userModel.Username;
            user.Email = userModel.Username + "@gmail.com";

            await _userManager.UpdateAsync(user);
            var getRole = _roleManager.FindById(user.Roles?.FirstOrDefault()?.RoleId);
            if(getRole?.Name != userModel.role)
            {
                if(getRole != null)
                {
                    var res1 = await _userManager.RemoveFromRoleAsync(user.Id, getRole?.Name);
                }
                var res2 = await _userManager.AddToRoleAsync(user.Id, userModel.role);
            }

            if (userModel.Password == null || userModel.Password == string.Empty) return;
            var token = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
            await _userManager.ResetPasswordAsync(userModel.UserId, token, userModel.Password);
        }

        public async Task<(bool success,Users user)> Login(string username, string password)
        {
            var res = await _userManager.FindAsync(username, password);
            if (res == null) return (false,null);
            return (true, res);
        }

        public IEnumerable<Users> GetUsers()
        {
            return _userManager.Users.Where(x => x.IsDeleted == false).ToList();
        }

        public async Task<Users> FindUserAsync(string userId)
        {
            var res = await _userManager.FindByIdAsync(userId);
            return res;
        }

        public void DeleteUser(string UserId)
        {
            _userManager.Delete(_userManager.FindById(UserId)); 
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                {
                    _userManager.Dispose(); 
                    _roleManager.Dispose();
                }
            disposed = true;
        }

        public async Task<Users> FindUserByUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
    }
}