using DBMS_Core.Extentions;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Validators
{
    public class TypeValidator
    {
        public static bool IsValidValue(SupportedTypes type, object value)
        {
            var currentType = Type.GetType(type.GetAssemblyDescription());

            try
            {
                Convert.ChangeType(value, currentType);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
