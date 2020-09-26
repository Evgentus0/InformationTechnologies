using DBMS_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Interfaces
{
    public interface IDataBaseService
    {
        DataBase DataBase { get; }
        ITableService this[string key] { get; }
        void AddTable(string tableName);
        void AddTable(string tableName, TableSchema schema);
        void DeleteTable(string tableName);
    }
}
