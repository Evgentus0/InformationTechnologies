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
        private string _url;
        public virtual string Url {
            get => _url;
            set 
            {
                _url = value;
                _dbClient = DbClientFactory.GetClient(_localhost, _db, _table);
            }
        }

        public string Type => typeof(SqlServerSource).AssemblyQualifiedName;

        public long SizeInBytes => default;

        public bool AllowMultipleSource => false;
        private IDbClient _dbClient;

        private string _localhost => Url.Split(Constants.Separator)[0];
        private string _db => Url.Split(Constants.Separator)[1];
        private string _table => Url.Split(Constants.Separator)[2];

        public void Create()
        {
            _dbClient.CreateTable();
        }

        public void Delete()
        {
            _dbClient.DeleteTable();
        }

        public List<List<object>> GetData()
        {
            var listStringData = _dbClient.GetData();

            var data = listStringData.Select(x => JsonSerializer.Deserialize<List<object>>(x)).ToList();
            return data;
        }

        public async Task<List<List<object>>> GetDataAsync()
        {
            var listStringData = await _dbClient.GetDataAsync();

            var data = listStringData.Select(x => JsonSerializer.Deserialize<List<object>>(x)).ToList();
            return data;
        }

        public void SetUrl(DataBase db, Table table)
        {
            Url = $"{db.Settings.RootPath}{Constants.Separator}{db.Name}{Constants.Separator}{table.Name}";
        }

        public void WriteData(List<List<object>> data)
        {
            if (!(data == null || data.Count == 0))
            {
                var newStringData = data.Select(x => JsonSerializer.Serialize(x)).ToList();
                _dbClient.ClearTable();
                _dbClient.InsertData(newStringData);
            }
        }

        public async Task WriteDataAsync(List<List<object>> data)
        {
            if (!(data == null || data.Count == 0))
            {
                var newStringData = data.Select(x => JsonSerializer.Serialize(x)).ToList();

                await _dbClient.InsertDataAsync(newStringData);
            }
        }
    }
}
