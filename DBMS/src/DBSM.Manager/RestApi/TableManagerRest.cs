using DBMS.WebApiClient;
using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using DBMS_Core.Models.Types;
using DBSM.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.Manager.RestApi
{
    class TableManagerRest: ITableManager
    {
        private Table _table;
        private string _tableName;
        private string _dbName;
        private Client _client;

        private bool _isTableDirty;
        public TableManagerRest(Table table, string dbName)
        {
            _table = table;
            _tableName = Table.Name;
            _dbName = dbName;
            _client = new Client();

            _isTableDirty = false;
        }

        public Table Table
        {
            get
            {
                if (_isTableDirty)
                {
                    _isTableDirty = false;
                    _table = _client.GetTable(_dbName, _tableName);
                }
                return _table;
            }
        }

        public void AddNewField(string name, SupportedTypes type, List<IValidator> validators = null)
        {
            validators = validators ?? new List<IValidator>();

            _isTableDirty = true;
            _client.AddField(_dbName, _tableName, name, type, validators);
        }

        public void AddNewField(Field field)
        {
            _isTableDirty = true;
            _client.AddField(_dbName, _tableName, field.Name, field.Type, field.Validators);
        }

        public void DeleteField(string name)
        {
            _isTableDirty = true;
            _client.DeleteField(_dbName, _tableName, name);
        }

        public void DeleteRows(Dictionary<string, List<IValidator>> conditions)
        {
            _client.DeleteRows(_dbName, _tableName, conditions);
        }

        public void DeleteRows(List<Guid> ids)
        {
            _client.DeleteRows(_dbName, _tableName, ids);
        }

        public void InsertData(List<object> row)
        {
            _client.InsertData(_dbName, _tableName, new List<List<object>> { row });
        }

        public void InsertDataRange(List<List<object>> rows)
        {
            _client.InsertData(_dbName, _tableName, rows);
        }

        public List<List<object>> Select()
        {
            return _client.Select(_dbName, _tableName);
        }

        public List<List<object>> Select(int top, int offset)
        {
            return _client.Select(_dbName, _tableName, top, offset);
        }

        public List<List<object>> Select(Dictionary<string, List<IValidator>> conditions)
        {
            return _client.Select(_dbName, _tableName, conditions);
        }

        public List<List<object>> Select(int top, int offset, Dictionary<string, List<IValidator>> conditions)
        {
            return _client.Select(_dbName, _tableName, top, offset, conditions);
        }

        public List<List<object>> Union(params Table[] tables)
        {
            return _client.Union(_dbName, _tableName, tables);
        }

        public void UpdateRows(List<List<object>> rows)
        {
            _client.UpdateRows(_dbName, _tableName, rows);
        }

        public void UpdateSchema()
        {
            _isTableDirty = true;
            _client.UpdateSchema(_dbName, Table);
        }
        public override string ToString()
        {
            return Table.Name;
        }
    }
}
