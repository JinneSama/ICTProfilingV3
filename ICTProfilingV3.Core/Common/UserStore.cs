using ICTProfilingV3.DataTransferModels.ServiceModels.DTOModels;

namespace ICTProfilingV3.Core.Common
{
    public class UserStore
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string UserRole { get; set; }
        public ArgumentCredentialsDto ArugmentCredentialsDto { get; set; }
    }
}
