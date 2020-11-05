using DBMS.SqlServerSource;
using DBMS.SqlServerSource.Clients;
using DBMS.SqlServerSource.Interfaces;
using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DBMS_Core.Sources
{
    class SqlServerSource : BaseSource
    {
        public override string Type => typeof(SqlServerSource).AssemblyQualifiedName;

        public override long SizeInBytes => default;

        public override bool AllowMultipleSource => false;

        protected override IDbClient DbClient
        {
            get
            {
                if (_dbClient == null)
                {
                    var data = Url.Split(Constants.Separator);
                    _dbClient = DbClientFactory.GetClient(data[0], data[1], data[2]);
                }

                return _dbClient;
            }
            set
            {
                _dbClient = value;
            }
        }

        public override void SetUrl(DataBase db, Table table)
        {
            Url = $"{db.Settings.RootPath}{Constants.Separator}{db.Name}{Constants.Separator}{table.Name}";
            Create();
        }
    }
}
