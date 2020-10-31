using DBMS_Core.Extentions;
using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using DBMS_Core.Models.Types;
using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DBMS_Core.Infrastructure.FileStore
{
    internal class FileWorker : IFileWorker
    {
        public DataBase DataBase { get; set; }
        private IDbWriter _dbWriter;
        public FileWorker(DataBase dataBase)
        {
            DataBase = dataBase;
            _dbWriter = DbWriterFactory.GetDbWriter(dataBase.Settings);
        }

        public DataBase GetDataBaseFromFile(string filePath)
        {
            return _dbWriter.GetDb(filePath);
        }

        public void AddNewColoumn(Table table, SupportedTypes type)
        {
            if (table.Sources.IsNullOrEmpty())
            {
                AddNewSource(table);
            }

            Parallel.ForEach(table.Sources, (source) =>
            {
                var data = source.GetData();
                data?.ForEach(x => x.Add(type.GetDefaultValue()));

                source.WriteData(data);
            });
        }

        public void DeleteField(Table table, int index)
        {
            if (!table.Sources.IsNullOrEmpty())
            {
                Parallel.ForEach(table.Sources, (source) =>
                {
                    var data = source.GetData();
                    
                    if(!data.IsNullOrEmpty())
                        data.ForEach(x => x.RemoveAt(index));

                    source.WriteData(data);
                });
            }
        }

        public void DeleteRows(Table table, Dictionary<string, List<IValidator>> conditions)
        {
            if (table.Sources.IsNullOrEmpty())
            {
                throw new Exception("The table is empty!");
            }

            Parallel.ForEach(table.Sources, (source) =>
            {
                var data = source.GetData();
                data.RemoveAll(element =>
                {
                    foreach (var condition in conditions)
                    {
                        var field = table.Schema.Fields.Find(x => x.Name == condition.Key);
                        var index = table.Schema.Fields.IndexOf(field) + 1;

                        if (!PassAllValidators(condition.Value, element[index]))
                        {
                            return false;
                        }
                    }
                    return true;
                });

                source.WriteData(data);
            });
        }


        public void DeleteRows(Table table, List<Guid> ids)
        {
            if (table.Sources.IsNullOrEmpty())
            {
                throw new Exception("The table is empty!");
            }

            Parallel.ForEach(table.Sources, (source) =>
            {
                var data = source.GetData();
                data.RemoveAll(element => ids.Contains(Guid.Parse(element[0].ToString())));

                source.WriteData(data);
            });
        }

        public void DeleteTableSources(Table table)
        {
            if (table.Sources.IsNullOrEmpty())
            {
                throw new Exception("The table is empty!");
            }

            Parallel.ForEach(table.Sources, source =>
            {
                source.Delete();
            });
        }

        public void InsertData(Table table, List<List<object>> rows)
        {
            if (!rows.IsNullOrEmpty())
            {
                if (table.Sources.IsNullOrEmpty() || (table.Sources.Last().AllowMultipleSource 
                    &&  table.Sources.Last().SizeInBytes >= DataBase.Settings.FileSize))
                {
                    AddNewSource(table);
                }

                rows = rows.Where(element =>
                {
                    for (int i = 0; i < element.Count; i++)
                    {
                        if (!PassAllValidators(table.Schema.Fields[i].Validators, element[i]))
                        {
                            return false;
                        }
                    }
                    return true;
                }).ToList();

                rows.ForEach(x => x.Insert(0, Guid.NewGuid()));

                var source = table.Sources.Last();

                var data = source.GetData();
                data.AddRange(rows);
                source.WriteData(data);
            }

        }

        public List<List<object>> Select(Table table)
        {
            List<List<object>> result = new List<List<object>>();

            if (table.Sources.IsNullOrEmpty())
            {
                return result;
            }

            foreach (var source in table.Sources)
            {
                result = result.Union(source.GetData()).ToList();
            }

            return result;
        }

        public List<List<object>> Select(Table table, int top, int offset)
        {
            List<List<object>> result = new List<List<object>>();

            if (table.Sources.IsNullOrEmpty())
            {
                return result;
            }

            foreach (var source in table.Sources)
            {
                var data = source.GetData();

                if(!data.IsNullOrEmpty())
                    result = result.Union(data).ToList();

                if (result.Count() + offset >= top)
                {
                    return result.Skip(offset).Take(top).ToList();
                }
            }

            return result.Skip(offset).ToList();
        }

        public List<List<object>> Select(Table table, Dictionary<string, List<IValidator>> conditions)
        {
            List<List<object>> result = new List<List<object>>();

            if (table.Sources.IsNullOrEmpty())
            {
                return result;
            }

            foreach (var source in table.Sources)
            {
                IEnumerable<List<object>> data = source.GetData();
                data = data.Where(element =>
                {
                    foreach (var condition in conditions)
                    {
                        var field = table.Schema.Fields.Find(x => x.Name == condition.Key);
                        var index = table.Schema.Fields.IndexOf(field) + 1;

                        if (!PassAllValidators(condition.Value, element[index]))
                        {
                            return false;
                        }
                    }
                    return true;
                });
                result = result.Union(data).ToList();
            }
            return result;
        }

        public List<List<object>> Select(Table table, int top, int offset, Dictionary<string, List<IValidator>> conditions)
        {
            List<List<object>> result = new List<List<object>>();

            if (table.Sources.IsNullOrEmpty())
            {
                return result;
            }

            foreach (var source in table.Sources)
            {
                IEnumerable<List<object>> data = source.GetData();
                data = data.Where(element =>
                {
                    foreach (var condition in conditions)
                    {
                        var field = table.Schema.Fields.Find(x => x.Name == condition.Key);
                        var index = table.Schema.Fields.IndexOf(field) + 1;

                        if (!PassAllValidators(condition.Value, element[index]))
                        {
                            return false;
                        }
                    }
                    return true;
                });
                result = result.Union(data).ToList();
                if (result.Count() + offset >= top)
                {
                    return result.Skip(offset).Take(top).ToList();
                }
            }
            return result.Skip(offset).ToList();
        }

        public void UpdateDataBaseFile()
        {
            _dbWriter.UpdateDb(DataBase);
        }

        public void UpdateRows(Table table, List<List<object>> rows)
        {
            if (table.Sources.IsNullOrEmpty())
            {
                throw new Exception("The table is empty!");
            }

            Parallel.ForEach(table.Sources, (source) =>
            {
                var data = source.GetData();
                var ids = rows.Select(x => (Guid)x[0]);

                data.RemoveAll(element => ids.Contains(Guid.Parse(element[0].ToString())));
                data.AddRange(rows);

                source.WriteData(data);
            });
        }

        private bool PassAllValidators(List<IValidator> validators, object value)
        {
            if (validators.IsNullOrEmpty())
            {
                return true;
            }

            foreach (var predicate in validators)
            {
                if (!predicate.IsValid(value))
                    return false;
            }
            return true;
        }

        private void AddNewSource(Table table)
        {
            var source = SourceFactory.GetSourceObject(DataBase.Settings.DefaultSource,
                    DataBase, table);

            table.Sources.Add(source);
            source.Create();

            UpdateDataBaseFile();
        }
    }
}
