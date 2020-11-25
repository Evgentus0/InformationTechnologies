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
    public abstract class BaseSource : ISource
    {
        public string Url { get; set; }

        public abstract string Type { get; }

        public abstract long SizeInBytes { get; }

        public abstract bool AllowMultipleSource { get; }

        protected IDbClient _dbClient;
        public IDbClientFactory DbClientFactory { get; set; }
        protected abstract IDbClient DbClient { get; set; }

        public BaseSource()
        { }

        public BaseSource(IDbClientFactory dbClientFactory)
        {
            DbClientFactory = dbClientFactory;
        }

        public abstract void SetUrl(DataBase db, Table table);

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

        public override bool Equals(object obj)
        {
            var source = (ISource)obj;

            return Type == source.Type
                && Url == source.Url;
        }
    }
}
