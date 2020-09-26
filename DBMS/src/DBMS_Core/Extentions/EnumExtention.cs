using DBMS_Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DBMS_Core.Extentions
{
    public static class EnumExtention
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        public static string GetAssemblyDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            AssemblyNameAttribute[] attributes = fi.GetCustomAttributes(typeof(AssemblyNameAttribute), false) as AssemblyNameAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Type.AssemblyQualifiedName;
            }

            return string.Empty;
        }

        public static string GetName(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }
    }
}
