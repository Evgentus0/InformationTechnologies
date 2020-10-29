using DBMS.SharedModels.DTO;
using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBMS.WebApi.Infrastructure.Interfaces
{
    public interface IFileHelper
    {
        Task<IDataBaseService> GetDb(string dbName);
        Task<List<string>> GetDbNames();
        Task CreateDb(DataBase db);
    }
}
