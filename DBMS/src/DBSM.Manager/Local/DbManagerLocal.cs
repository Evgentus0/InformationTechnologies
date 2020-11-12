using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using DBSM.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBSM.Manager.Local
{
    class DbManagerLocal : IDbManager
    {
        private IDataBaseService _dataBaseService;

        public DbManagerLocal(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public ITableManager this[string key] => new TableManagerLocal(_dataBaseService[key]);

        public string Name => _dataBaseService.Name;

        public void AddTable(string tableName)
        {
            _dataBaseService.AddTable(tableName);
        }

        public void AddTable(string tableName, TableSchema schema)
        {
            _dataBaseService.AddTable(tableName, schema);
        }

        public void DeleteTable(string tableName)
        {
            _dataBaseService.DeleteTable(tableName);
        }

        public IEnumerable<ITableManager> GetTables()
        {
            return _dataBaseService.GetTables().Select(x => new TableManagerLocal(x));
        }
    }
}
