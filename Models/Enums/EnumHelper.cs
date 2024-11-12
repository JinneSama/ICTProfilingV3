using System.ComponentModel;
using System;
using System.Linq;

namespace Models.Enums
{
    public static class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
