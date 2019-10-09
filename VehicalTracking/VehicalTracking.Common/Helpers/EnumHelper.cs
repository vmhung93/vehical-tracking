using System;
using System.ComponentModel;
using System.Linq;

namespace VehicalTracking.Common.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescriptionFromEnumValue(this Enum value)
        {
            return value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() is DescriptionAttribute descriptionAttribute ? descriptionAttribute.Description : value.ToString();
        }
    }
}
