using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.SqlServerSource.Interfaces
{
    public interface IDbClient
    {
        void UpdateOrCreateDb(string stringDbData);
        void CreateTable();
        void DeleteTable();
        List<string> GetData();
        Task<List<string>> GetDataAsync();
        void InsertData(List<string> data);
        Task InsertDataAsync(List<string> data);
        void ClearTable();
        string GetDb();
    }
}
