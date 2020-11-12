using DBMS_Core.Models;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Interfaces
{
    public interface ITableService
    {
        Table Table { get; }
        void UpdateSchema();

        void AddNewField(string name, SupportedTypes type, List<IValidator> validators = null);
        void AddNewField(Field field);
        void DeleteField(string name);

        void InsertData(List<object> row);
        void InsertDataRange(List<List<object>> rows);
        void UpdateRows(List<List<object>> rows);
        void DeleteRows(Dictionary<string, List<IValidator>> conditions);
        void DeleteRows(List<Guid> ids);

        List<List<object>> Select();
        List<List<object>> Select(int top, int offset);
        List<List<object>> Select(Dictionary<string, List<IValidator>> conditions);
        List<List<object>> Select(int top, int offset, Dictionary<string, List<IValidator>> conditions);

        List<List<object>> Union(params Table[] tables);
    }
}
