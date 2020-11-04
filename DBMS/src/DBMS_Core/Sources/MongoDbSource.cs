using DBMS.SqlServerSource;
using DBMS.SqlServerSource.Interfaces;
using DBMS_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Sources
{
    class MongoDbSource: SqlServerSource
    {
        public override string Type => typeof(MongoDbSource).AssemblyQualifiedName;

        protected override IDbClient DbClient
        {
            get
            {
                if (_dbClient == null)
                {
                    var data = Url.Split(Constants.Separator);
                    _dbClient = DbClientFactory.GetMongoClient(data[0], data[1], data[2]);
                }

                return _dbClient;
            }
            set
            {
                _dbClient = value;
            }
        }
    }
}
