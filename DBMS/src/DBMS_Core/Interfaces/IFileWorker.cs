using DBMS_Core.Models;
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
        void AddNewColoumn(Table table);
        void DeleteField(Table table, int index);
        void DeleteRows(Table table, Dictionary<string, List<IValidator>> conditions);
        void InsertData(Table table, List<List<object>> row);
        IEnumerable<List<object>> Select(Table table);
        IEnumerable<List<object>> Select(Table table, int top, int offset);
        IEnumerable<List<object>> Select(Table table, Dictionary<string, List<IValidator>> conditions);
        IEnumerable<List<object>> Select(Table table, int top, int offset, Dictionary<string, List<IValidator>> conditions);
    }
}
