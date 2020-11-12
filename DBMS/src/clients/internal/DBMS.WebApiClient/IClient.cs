using DBMS_Core.Interfaces;
using DBMS_Core.Models.Types;
using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.Text;
using DBMS_Core.Models;

namespace DBMS.WebApiClient
{
    public interface IClient
    {
        void AddTable(string dbName, string tableName);
        void DeleteTable(string dbName, string tableName);
        void CreateDb(string name, long fileSize, SupportedSources source);
        Table GetTable(string dbName, string tableName);
        IEnumerable<Table> GetTables(string dbName);
        void AddField(string dbName, string tableName, string fieldName, SupportedTypes type, List<IValidator> validators);
        void DeleteField(string dbName, string tableName, string name);
        void DeleteRows(string dbName, string tableName, Dictionary<string, List<IValidator>> conditions);
        void DeleteRows(string dbName, string tableName, List<Guid> ids);
        void InsertData(string dbName, string tableName, List<List<object>> lists);
        List<List<object>> Select(string dbName, string tableName);
        List<List<object>> Select(string dbName, string tableName, int top, int offset);
        List<List<object>> Select(string dbName, string tableName, Dictionary<string, List<IValidator>> conditions);
        List<List<object>> Select(string dbName, string tableName, int top, int offset, Dictionary<string, List<IValidator>> conditions);
        List<List<object>> Union(string dbName, string tableName, Table[] tables);
        void UpdateRows(string dbName, string tableName, List<List<object>> rows);
        void UpdateSchema(string dbName, Table table);
        List<string> GetDbsList();
    }
}
