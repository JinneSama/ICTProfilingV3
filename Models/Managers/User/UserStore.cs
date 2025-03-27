using Models.Entities;
using Models.Enums;
using Models.Repository;
using Models.Service.DTOModels;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Managers.User
{
    public static class UserStore
    {
        public static string UserId { get; set; }
        public static string Username { get; set; }
        public static string Fullname { get; set; }
        public static async Task<Sections?> Section() 
        {
            var res = await GetStaff();
            return res?.Section;
        }
        private static async Task<ITStaff> GetStaff()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var res = await unitOfWork.ITStaffRepo.FindAsync(x => x.UserId == UserId);
            if (res == null) return null;
            return res;
        }
        public static ArgumentCredentialsDto ArugmentCredentialsDto { get; set; }
    }
}
