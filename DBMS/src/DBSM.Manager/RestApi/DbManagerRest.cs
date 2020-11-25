using DBMS.WebApiClient;
using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using DBSM.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMS.Manager.RestApi
{
    public class DbManagerRest : IDbManager
    {
        private IClient _client;

        public ITableManager this[string key]
        {
            get
            {
                Table tableService = _client.GetTable(Name, key);

                return new TableManagerRest(tableService, Name, _client);
            }
        }

        public string Name { get; }

        public DbManagerRest(string dbName, IClient client)
        {
            Name = dbName;
            _client = client;
        }

        public void AddTable(string tableName)
        {
            _client.AddTable(Name, tableName);
        }

        public void DeleteTable(string tableName)
        {
            _client.DeleteTable(Name, tableName);
        }

        public IEnumerable<ITableManager> GetTables()
        {
            return _client.GetTables(Name).Select(x => new TableManagerRest(x, Name, _client));
        }
    }
}
