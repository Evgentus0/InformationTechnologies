using DBMS_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBSM.Manager.Interfaces
{
    public interface IDbManager
    {
        string Name { get; }
        ITableManager this[string key] { get; }
        void AddTable(string tableName);
        void AddTable(string tableName, TableSchema schema);
        void DeleteTable(string tableName);
        IEnumerable<ITableManager> GetTables();
    }
}
