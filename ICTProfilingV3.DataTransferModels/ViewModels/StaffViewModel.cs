using Models.Entities;
using Models.Enums;
using System;
using System.Drawing;

namespace ICTProfilingV3.DataTransferModels.ViewModels
{
    public class StaffViewModel
    {
        public Users Users => Staff?.Users;
        public Image Image { get; set; }
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
