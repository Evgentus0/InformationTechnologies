using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Extentions;
using DBMS_Core.Infrastructure.Validators;
using System.Runtime.CompilerServices;

namespace DBMS_Core.Infrastructure.Services
{
    internal class TableService : ITableService
    {
        public Table Table { get;  private set; }

        private IFileWorker _fileWorker;

        public TableService(Table table, IFileWorker fileWorker)
        {
            Table = table;

            _fileWorker = fileWorker;
        }

        public void UpdateSchema()
        {
            _fileWorker.UpdateDataBaseFile();
        }

        public void UpdateRows(List<List<object>> rows)
        {
            for(int i = 0; i < rows.Count; i++)
            {
                if(!ValidateDataTypes(rows[i].Skip(1).ToList()))
                    throw new ArgumentException("Incorrect data types! Expected: " +
                    Table.Schema.Fields.Select(x => x.Type.GetName()).Aggregate((x, y) => $"{x}, {y}"));
            }

            _fileWorker.UpdateRows(Table, rows);
        }

        public void AddNewField(string name, SupportedTypes type, List<IValidator> validators = null)
        {
            if (Table.Schema.Fields.Select(x => x.Name.ToLower()).Any(x => x == name.ToLower()))
                throw new ArgumentException($"Field with name: {name} already exist!");

            Table.Schema.Fields.Add(new Field { Name = name, Type = type, Validators = validators });
            _fileWorker.UpdateDataBaseFile();

            _fileWorker.AddNewColoumn(Table, type);
        }

        public void AddNewField(Field field)
        {
            Table.Schema.Fields.Add(field);
            _fileWorker.UpdateDataBaseFile();

            _fileWorker.AddNewColoumn(Table, field.Type);
        }

        public void DeleteField(string name)
        {
            var field = Table.Schema.Fields.Find(x => x.Name == name);
            var index = Table.Schema.Fields.IndexOf(field) + 1;
            Table.Schema.Fields.Remove(field);
            _fileWorker.UpdateDataBaseFile();

            _fileWorker.DeleteField(Table, index);
        }

        public void DeleteRows(Dictionary<string, List<IValidator>> conditions)
        { 
            _fileWorker.DeleteRows(Table, conditions);
        }
        public void DeleteRows(List<Guid> ids)
        {
            _fileWorker.DeleteRows(Table, ids);
        }

        public void InsertData(List<object> row)
        {
            if (!ValidateDataTypes(row))
                throw new ArgumentException("Incorrect data types! Expected: " + 
                    Table.Schema.Fields.Select(x => x.Type.GetName()).Aggregate((x, y) => $"{x}, {y}"));

            _fileWorker.InsertData(Table, new List<List<object>> { row });
        }

        public void InsertDataRange(List<List<object>> rows)
        {
            if (!rows.All(x => ValidateDataTypes(x)))
                throw new ArgumentException("Incorrect data types! Expected: " +
                    Table.Schema.Fields.Select(x => x.Type.GetName()).Aggregate((x, y) => $"{x}, {y}"));

            _fileWorker.InsertData(Table, rows);
        }

        public List<List<object>> Select()
        {
            return _fileWorker.Select(Table);
        }

        public List<List<object>> Select(int top, int offset)
        {
            return _fileWorker.Select(Table, top, offset);
        }

        public List<List<object>> Select(Dictionary<string, List<IValidator>> conditions)
        {
            return _fileWorker.Select(Table, conditions);
        }

        public List<List<object>> Select(int top, int offset, Dictionary<string, List<IValidator>> conditions)
        {
            return _fileWorker.Select(Table, top, offset, conditions);
        }

        public List<List<object>> Union(params Table[] tables)
        {
            IEnumerable<List<object>> result = _fileWorker.Select(this.Table);
            List<Field> exampleFields = this.Table.Schema.Fields;

            foreach(var table in tables)
            {
                if(!MatchField(exampleFields, table.Schema.Fields))
                {
                    throw new ArgumentException($"Fields from table {table.Name} does not match with the first table!");
                }

                result = result.Union(_fileWorker.Select(table));
            }

            return result.ToList();
        }

        private bool MatchField(List<Field> fields1, List<Field> fields2)
        {
            if(fields1.Count == fields2.Count)
            {
                for(int i = 0; i < fields1.Count; i++)
                {
                    if(fields1[i].Type != fields2[i].Type)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        private bool ValidateDataTypes(List<object> row)
        {
            var fields = Table.Schema.Fields;
            
            if(fields.Count == row.Count)
            {
                for(int i = 0; i < fields.Count; i++)
                {
                    if (!TypeValidator.IsValidValue(fields[i].Type, row[i]))
                        return false;
                }
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return Table.Name;
        }
    }
}
