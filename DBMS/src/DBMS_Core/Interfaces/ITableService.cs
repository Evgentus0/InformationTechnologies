using DBMS_Core.Models;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Interfaces
{
    public interface ITableService
    {
        string Name { get; }
        List<Field> Fields { get; }
        void AddNewField(string name, SupportedTypes type, List<IValidator> validators = null);
        void AddNewField(Field field);
        void DeleteField(string name);

        void InsertData(List<object> row);
        void InsertDataRange(List<List<object>> rows);
        void DeleteRows(Dictionary<string, List<IValidator>> conditions);
        //void UpdateRow(Dictionary<string, object> keyValues, List<List<object>> rows);

        IEnumerable<List<object>> Select();
        IEnumerable<List<object>> Select(int top, int offset);
        IEnumerable<List<object>> Select(Dictionary<string, List<IValidator>> conditions);
        IEnumerable<List<object>> Select(int top, int offset, Dictionary<string, List<IValidator>> conditions);

        List<List<object>> Union(params Table[] tables);
    }
}
