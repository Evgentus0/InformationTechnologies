using DBMS_Core.Attributes;
using DBMS_Core.Models.Types;
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

        public static string GetAssemblyDescription(this Enum value, string key)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            AssemblyNameAttribute[] attributes = fi.GetCustomAttributes(typeof(AssemblyNameAttribute), false) as AssemblyNameAttribute[];

            if (!attributes.IsNullOrEmpty())
            {
                return attributes.FirstOrDefault(x => x.Key == key).Type.AssemblyQualifiedName;
            }

            return string.Empty;
        }

        public static string GetValidatorType(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            ValidatorTypeAttribute[] attributes = fi.GetCustomAttributes(typeof(ValidatorTypeAttribute), false) as ValidatorTypeAttribute[];

            if (!attributes.IsNullOrEmpty())
            {
                return attributes.First().ValidatorType.AssemblyQualifiedName;
            }

            return string.Empty;
        }

        public static bool IsValidatorAvailable(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            ValidatorTypeAttribute[] attributes = fi.GetCustomAttributes(typeof(ValidatorTypeAttribute), false) as ValidatorTypeAttribute[];

            return !attributes.IsNullOrEmpty();
        }

        public static string GetName(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }

        public static object GetDefaultValue(this SupportedTypes value)
        {
            var type = Type.GetType(value.GetAssemblyDescription(Constants.TypeDescription));
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}
