using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Extentions
{
    public static class IEnumerableExtention
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> list)
        {
            return list == null || list.Count == 0;
        }
    }
}
