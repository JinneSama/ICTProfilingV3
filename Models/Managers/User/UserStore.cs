using Models.Repository;
using System.Threading.Tasks;

namespace Models.Managers.User
{
    public static class UserStore
    {
        public static string UserId { get; set; }
        public static string Username { get; set; }
        public static string Fullname { get; set; }
        public static int? StaffId => GetStaffId().Result.Value;

        private static async Task<int?> GetStaffId()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var res = await unitOfWork.ITStaffRepo.FindAsync(x => x.UserId == UserId);
            if (res == null) return null;
            return res.Id;
        }
    }
}
