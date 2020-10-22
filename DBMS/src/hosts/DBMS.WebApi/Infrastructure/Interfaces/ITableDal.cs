using DBMS.SharedModels.DTO;
using DBMS.SharedModels.ResuestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBMS.WebApi.Infrastructure.Interfaces
{
    public interface ITableDal
    {
        Task<RequestResult> AddField(string tableName, string dbName, Field field);
        Task<RequestResult> DeleteField(string tableName, string dbName, string fieldName);
        Task<RequestResult> InsertData(string tableName, string dbName, List<List<object>> data);
        Task<RequestResult> UpdateData(string tableName, string dbName, List<List<object>> data);
        Task<RequestResult> Delete(string tableName, string dbName, Dictionary<string, List<Validator>> conditions);
        Task<RequestResult> Delete(string tableName, string dbName, List<Guid> ids);
        Task<(List<List<object>> data, RequestResult result)> Select(string tableName, string dbName, SelectRequest requestParameters);
        Task<(List<List<object>> data, RequestResult result)> Union(string tableName, string dbName, List<string> tableNames);
        Task<RequestResult> UpdateSchema(string tableName, string dbName, Table table);
    }
}
