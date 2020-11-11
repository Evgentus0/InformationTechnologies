using DBMS.SqlServerSource.Clients;
using DBMS.SqlServerSource.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.SqlServerSource
{
    public class DbClientFactory: IDbClientFactory
    {
        public IDbClient GetSqlClient(string localhost, string db, string table = "")
        {
            return new DbClient(localhost, db, table);
        }

        public IDbClient GetMongoClient(string localhost, string db, string table = "")
        {
            return new MongoDbClient(localhost, db, table);
        }

        public IDbClient GetJsonClient(string path)
        {
            return new JsonDbClient(path);
        }
    }
}
