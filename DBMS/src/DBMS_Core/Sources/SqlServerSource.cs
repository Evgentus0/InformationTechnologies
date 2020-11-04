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
    class SqlServerSource : ISource
    {
        public string Url { get; set; }

        public virtual string Type => typeof(SqlServerSource).AssemblyQualifiedName;

        public long SizeInBytes => default;

        public bool AllowMultipleSource => false;
        protected IDbClient _dbClient;
        protected virtual IDbClient DbClient
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

        protected void Create()
        {
            DbClient.CreateTable();
        }

        public void Delete()
        {
            DbClient.DeleteTable();
        }

        public List<List<object>> GetData()
        {
            var listStringData = DbClient.GetData();

            var data = listStringData.Select(x => JsonSerializer.Deserialize<List<object>>(x)).ToList();
            return data;
        }

        public async Task<List<List<object>>> GetDataAsync()
        {
            var listStringData = await DbClient.GetDataAsync();

            var data = listStringData.Select(x => JsonSerializer.Deserialize<List<object>>(x)).ToList();
            return data;
        }

        public void SetUrl(DataBase db, Table table)
        {
            Url = $"{db.Settings.RootPath}{Constants.Separator}{db.Name}{Constants.Separator}{table.Name}";
            Create();
        }

        public void WriteData(List<List<object>> data)
        {
            if (!(data == null || data.Count == 0))
            {
                var newStringData = data.Select(x => JsonSerializer.Serialize(x)).ToList();
                DbClient.ClearTable();
                DbClient.InsertData(newStringData);
            }
        }

        public async Task WriteDataAsync(List<List<object>> data)
        {
            if (!(data == null || data.Count == 0))
            {
                var newStringData = data.Select(x => JsonSerializer.Serialize(x)).ToList();

                await DbClient.InsertDataAsync(newStringData);
            }
        }
    }
}
