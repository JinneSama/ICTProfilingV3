using Models.Entities;
using Models.Enums;
using System;
using System.Drawing;

namespace Models.ViewModels
{
    public class StaffViewModel
    {
        public Users Users { get; set; }
        public Image Image => string.IsNullOrWhiteSpace(Staff.ImagePath) ? null : Image.FromFile(Staff.ImagePath);
        public ITStaff Staff { get; set; }
        public bool? Mark { get; set; }
        public string Initials => GetInitials(Users.FullName);
        public Sections Section => Staff.Section;

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
