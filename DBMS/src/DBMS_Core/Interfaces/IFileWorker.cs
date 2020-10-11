using DBMS_Core.Models;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DBMS_Core.Interfaces
{
    public interface IFileWorker
    {
        DataBase DataBase { get; set; }
        DataBase GetDataBaseFromFile(string filePath);
        void UpdateDataBaseFile();
        void DeleteTableSources(Table table);
        void AddNewColoumn(Table table, SupportedTypes type);
        void DeleteField(Table table, int index);
        void DeleteRows(Table table, Dictionary<string, List<IValidator>> conditions);
        void InsertData(Table table, List<List<object>> row);
        List<List<object>> Select(Table table);
        List<List<object>> Select(Table table, int top, int offset);
        List<List<object>> Select(Table table, Dictionary<string, List<IValidator>> conditions);
        List<List<object>> Select(Table table, int top, int offset, Dictionary<string, List<IValidator>> conditions);
        void DeleteRows(Table table, List<Guid> ids);
        void UpdateRows(Table table, List<List<object>> rows);
    }
}
