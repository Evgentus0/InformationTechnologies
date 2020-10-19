using DBMS_Core.Interfaces;
using DBSM.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMSM.Comparers
{
    public class TableServiceComparer : IEqualityComparer<ITableManager>
    {
        public TableServiceComparer()
        {

        }

        public bool Equals(ITableManager x, ITableManager y)
        {
            return x.Table.Name == y.Table.Name;
        }

        public int GetHashCode(ITableManager obj)
        {
            return obj.Table.Name.ToCharArray().Select(x => (int)x).Aggregate((x, y) => x ^ y);
        }
    }
}
