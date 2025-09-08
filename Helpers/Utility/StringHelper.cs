using System;

namespace Helpers.Utility
{
    public class StringHelper
    {
        public static string GetInitials(string fullName)
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
