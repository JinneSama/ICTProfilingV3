using Models.Entities;
using Models.Enums;
using static System.Net.Mime.MediaTypeNames;
using System;

namespace ICTProfilingV3.DataTransferModels
{
    public class StaffDTM
    {
        public Users Users => Staff?.Users;
        public ITStaff Staff { get; set; }
        public bool? Mark { get; set; }
        public string Initials => GetInitials(Users.FullName);
        public Sections Section => Staff.Section;
        public string UserId { get; set; }

        static string GetInitials(string fullName)
        {
            string[] nameParts = fullName.Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
            string initials = string.Empty;
            foreach (string part in nameParts)
            {
                if (!string.IsNullOrEmpty(part))
                {
                    initials += part[0];
                }
            }
            return initials.ToUpper();
        }
    }
}
