using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DBMS.SqlServerSource.Extentions
{
    static class StringExtentions
    {
        public static string WithParameters(this string @string, params object[] parameters)
        {
            return string.Format(@string, parameters);
        }
    }
}
