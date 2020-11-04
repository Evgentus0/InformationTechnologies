using DBMS.SqlServerSource;
using DBMS.SqlServerSource.Interfaces;
using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DBMS_Core.Sources
{
    class JsonSource : BaseSource
    {
        public override string Type => typeof(JsonSource).AssemblyQualifiedName;

        public override long SizeInBytes => new FileInfo(Url).Length;

        public override bool AllowMultipleSource => true;

        protected override IDbClient DbClient 
        {
            get
            {
                if (_dbClient == null)
                {
                    _dbClient = DbClientFactory.GetJsonClient(Url);
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
            Url = $"{db.Settings.RootPath}\\{table.Name}{Constants.TableFileExtention}";
            Create();
        }
    }
}
