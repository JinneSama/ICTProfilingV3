using ICTProfilingV3.DataTransferModels.ServiceModels.DTOModels;
using Models.Entities;
using Models.Enums;
using Models.Repository;
using System.Threading.Tasks;

namespace ICTProfilingV3.Core.Common
{
    public class UserStore
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public async Task<Sections?> Section() 
        {
            var res = await GetStaff();
            return res?.Section;
        }
        private async Task<ITStaff> GetStaff()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            var res = await unitOfWork.ITStaffRepo.FindAsync(x => x.UserId == UserId);
            if (res == null) return null;
            return res;
        }
        public ArgumentCredentialsDto ArugmentCredentialsDto { get; set; }
    }
}
