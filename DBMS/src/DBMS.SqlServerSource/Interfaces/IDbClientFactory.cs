using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.SqlServerSource.Interfaces
{
    public interface IDbClientFactory
    {
        IDbClient GetSqlClient(string localhost, string db, string table = "");
        IDbClient GetMongoClient(string localhost, string db, string table = "");
        IDbClient GetJsonClient(string path);
    }
}
