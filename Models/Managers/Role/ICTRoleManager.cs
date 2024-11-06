using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Entities;
using Models.Enums;

namespace EntityManager.Managers.Role
{
    public class ICTRoleManager : IICTRoleManager , IDisposable
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private bool disposed;

        public ICTRoleManager()
        {
            _roleManager = RoleManager.Create();
            _userManager = UserManager.Create();
        }

        public async Task CreateRole(string role, string Designation)
        {
            var _role = new Roles{ Name = role };
            var designations = SeparateDesignations(_role.Id, Designation);
            _role.RoleDesignations = designations;
            await _roleManager.CreateAsync(_role);
        }

        public async Task DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            await _roleManager.DeleteAsync(role);
        }

        public async Task UpdateRole(string roleId, string Designation)
        {
            var role = await _roleManager.FindByIdAsync(roleId) as Roles; 
            var designations = SeparateDesignations(role.Id, Designation);
            role.RoleDesignations = designations;
            await _roleManager.UpdateAsync(role);
        }

        public async Task AssignRoleToUser(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return;

            var res = await _userManager.AddToRoleAsync(user.Id, role);
            var err = res.Errors;    
        }

        public async Task RemoveRoleFromUser(string userId, string role)
        {
            var user = _userManager.FindByIdAsync(userId);
            if (user == null) return;

            await _userManager.RemoveFromRoleAsync(userId, role);
        }

        public IEnumerable<Roles> GetRoles()
        {
            return _roleManager.Roles.OfType<Roles>();
        }

        private List<RoleDesignation> SeparateDesignations(string roleId, string Designations)
        {
            var roles = Designations.Split(',');
            List<RoleDesignation> roleCollection = new List<RoleDesignation>(); 
            foreach (var role in roles)
            {
                var roleDesignation = new RoleDesignation
                {
                    RoleId = roleId,
                    Designation = (Designation)Enum.Parse(typeof(Designation), role)
                };
                roleCollection.Add(roleDesignation);
            }
            return roleCollection;
        }
        public async Task<IdentityRole> FindById(string id)
        {
            var res = await _roleManager.FindByIdAsync(id);
            return res;
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
                    _roleManager.Dispose();
                    _userManager.Dispose();
                }
            disposed = true;
        }

    }
}