using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMS_Core.Comparers
{
    public class TableServiceComparer : IEqualityComparer<ITableService>
    {
        public TableServiceComparer()
        {

        }

        public bool Equals(ITableService x, ITableService y)
        {
            return x.Table.Name == y.Table.Name;
        }

        public int GetHashCode(ITableService obj)
        {
            return obj.Table.Name.ToCharArray().Select(x => (int)x).Aggregate((x, y) => x ^ y);
        }
    }
}
