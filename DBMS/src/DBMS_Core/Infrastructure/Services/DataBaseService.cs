using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Infrastructure.Factories.Interfaces;
using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using DBMS_Core.Sources;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using static DBMS_Core.Constants;

namespace DBMS_Core.Infrastructure.Services
{
    internal class DataBaseService: IDataBaseService
    {
        private IFileWorker _fileWorker;

        private ITableServiceFactory _tableServiceFactory;
        private IDbWriterFactory _dbWriterFactory;

        private DataBase DataBase {  get;  set; }

        public string Name => DataBase.Name;
        public ITableService this[string tableName]
        {
            get
            {
                return _tableServiceFactory.GetTableService(DataBase.Tables.Find(x => x.Name == tableName), _fileWorker);
            }
        }

        private string _dataBaseFile => $"{DataBase.Name}{DataBaseFileExtention}";

        public DataBaseService(string name, string rootPath, long fileSize, SupportedSources source, 
            IFileWorkerFactory fileWorkerFactory,
            ITableServiceFactory tableServiceFactory,
            IDbWriterFactory dbWriterFactory)
        {
            _tableServiceFactory = tableServiceFactory;
            _dbWriterFactory = dbWriterFactory;

            var regex = new Regex(".*"); //valid 

            if (regex.IsMatch(rootPath))
            {
                DataBase = new DataBase 
                { 
                    Name = name, 
                    Settings = new Settings { RootPath = rootPath, FileSize = fileSize, DefaultSource = source } 
                };
                _fileWorker = fileWorkerFactory.GetFileWorker(DataBase);

                _fileWorker.UpdateDataBaseFile();
            }
            else
            {
                throw new ArgumentException($"Incorrect path: {rootPath}");
            }
        }

        public DataBaseService(string path, 
            IFileWorkerFactory fileWorkerFactory,
            ITableServiceFactory tableServiceFactory,
            IDbWriterFactory dbWriterFactory)
        {
            _tableServiceFactory = tableServiceFactory;
            _dbWriterFactory = dbWriterFactory;

            SupportedSources source = ResolvePath(path);

            _fileWorker = fileWorkerFactory.GetFileWorker(new DataBase { Settings = new Settings { DefaultSource = source} });
            DataBase = _fileWorker.GetDataBaseFromFile(path);

            _fileWorker.DataBase = DataBase;
        }

        private SupportedSources ResolvePath(string path)
        {
            SupportedSources source;
            if (path.Split(Constants.Separator).Length == 2)
            {
                if (path.Split(Constants.Separator)[0].StartsWith("mongodb"))
                    source = SupportedSources.MongoDb;
                else
                    source = SupportedSources.SqlServer;
            }
            else
                source = SupportedSources.Json;
            return source;
        }

        public void AddTable(string tableName)
        {
            if (DataBase.Tables.Any(x => x.Name == tableName))
                throw new ArgumentException($"Table with name: {tableName} already exist!");

            DataBase.Tables.Add(new Table { Name = tableName });
            _fileWorker.UpdateDataBaseFile();
        }

        public void AddTable(string tableName, TableSchema schema)
        {
            DataBase.Tables.Add(new Table { Name = tableName, Schema = schema });
            _fileWorker.UpdateDataBaseFile();
        }

        public void DeleteTable(string tableName)
        {
            var table = DataBase.Tables.First(x => x.Name == tableName);
            _fileWorker.DeleteTableSources(table);
            DataBase.Tables.Remove(table);
            _fileWorker.UpdateDataBaseFile();
        }

        public IEnumerable<ITableService> GetTables()
        {
            foreach(var table in DataBase.Tables)
            {
                yield return _tableServiceFactory.GetTableService(table, _fileWorker);
            }
        }

        public void DeleteDb()
        {
            var tables = DataBase.Tables.ToList();

            foreach (var table in tables)
            {
                DeleteTable(table.Name);
            }

            _dbWriterFactory.GetDbWriter(DataBase.Settings.DefaultSource).DeleteDb(DataBase);

            this.DataBase = null;
        }
    }
}
