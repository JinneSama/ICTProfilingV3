using System;
using System.Drawing;

namespace Models.Models
{
    public class StaffModel
    {
        public Image Image { get; set; }
        public string AssignedTo { get; set; }
        public string FullName { get; set; }
        public bool PhotoVisible {  get; set; }
        public bool InitialsVisible { get; set; }
        public string Initials => GetInitials(FullName);
        private string GetInitials(string fullName)
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
