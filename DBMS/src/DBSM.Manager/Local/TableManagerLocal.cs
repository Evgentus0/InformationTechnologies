using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using DBMS_Core.Models.Types;
using DBSM.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBSM.Manager.Local
{
    class TableManagerLocal : ITableManager
    {
        private ITableService _tableService;

        public TableManagerLocal(ITableService tableService)
        {
            _tableService = tableService;
        }

        public Table Table => _tableService.Table;

        public void AddNewField(string name, SupportedTypes type, List<IValidator> validators = null)
        {
            _tableService.AddNewField(name, type, validators);
        }

        public void AddNewField(Field field)
        {
            _tableService.AddNewField(field);
        }

        public void DeleteField(string name)
        {
            _tableService.DeleteField(name);
        }

        public void DeleteRows(Dictionary<string, List<IValidator>> conditions)
        {
            _tableService.DeleteRows(conditions);
        }

        public void DeleteRows(List<Guid> ids)
        {
            _tableService.DeleteRows(ids);
        }

        public void InsertData(List<object> row)
        {
            _tableService.InsertData(row);
        }

        public void InsertDataRange(List<List<object>> rows)
        {
            _tableService.InsertDataRange(rows);
        }

        public List<List<object>> Select()
        {
            return _tableService.Select();
        }

        public List<List<object>> Select(int top, int offset)
        {
            return _tableService.Select(top, offset);
        }

        public List<List<object>> Select(Dictionary<string, List<IValidator>> conditions)
        {
            return _tableService.Select(conditions);
        }

        public List<List<object>> Select(int top, int offset, Dictionary<string, List<IValidator>> conditions)
        {
            return _tableService.Select(top, offset, conditions);
        }

        public List<List<object>> Union(params Table[] tables)
        {
            return _tableService.Union(tables);
        }

        public void UpdateRows(List<List<object>> rows)
        {
            _tableService.UpdateRows(rows);
        }

        public void UpdateSchema()
        {
            _tableService.UpdateSchema();
        }
        public override string ToString()
        {
            return Table.Name;
        }
    }
}
