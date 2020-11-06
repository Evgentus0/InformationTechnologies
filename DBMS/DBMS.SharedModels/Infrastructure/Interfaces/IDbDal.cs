using DBMS.SharedModels.DTO;
using DBMS.SharedModels.ResuestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBMS.SharedModels.Infrastructure.Interfaces
{
    public interface IDbDal
    {
        Task<RequestResult> AddTable(string dbName, string tableName);
        Task<RequestResult> DeleteTable(string dbName, string tableName);
        Task<(Table table, RequestResult result)> GetTable(string dbName, string tableName);
        Task<(List<Table> tables, RequestResult result)> GetTables(string dbName);
        Task<(DataBase db, RequestResult result)> GetDataBase(string dbName);
        Task<(List<string> dbs, RequestResult result)> GetDataBasesNames();
        Task<RequestResult> CreateDb(DataBase db);
    }
}
