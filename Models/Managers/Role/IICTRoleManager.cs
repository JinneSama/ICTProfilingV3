using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Entities;

namespace EntityManager.Managers.Role
{
    public interface IICTRoleManager
    {
        Task CreateRole(string role , string Designation);
        Task DeleteRole(string roleId);
        Task UpdateRole(string roleId , string Designation);
        Task AssignRoleToUser(string userId, string role);
        Task RemoveRoleFromUser(string userId, string role);
        Task<IdentityRole> FindById(string id);
        IEnumerable<Roles> GetRoles();
    }
}