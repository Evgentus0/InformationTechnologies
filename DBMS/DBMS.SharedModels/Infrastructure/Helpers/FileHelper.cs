using DBMS.SharedModels.DTO;
using DBMS.SharedModels.Infrastructure.Interfaces;
using DBMS.SqlServerSource;
using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Infrastructure.Factories.Interfaces;
using DBMS_Core.Interfaces;
using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core = DBMS_Core;

namespace DBMS.SharedModels.Infrastructure.Helpers
{
    public class FileHelper : IFileHelper
    {
        private Settings.Settings _setting;
        private IDbWriterFactory _dbWriterFactory;
        private IDataBaseServiceFactory _dataBaseServiceFactory;

        public FileHelper(Settings.Settings setting, 
            IDbWriterFactory dbWriterFactory,
            IDataBaseServiceFactory dataBaseServiceFactory)
        {
            _setting = setting;
            _dbWriterFactory = dbWriterFactory;
            _dataBaseServiceFactory = dataBaseServiceFactory;
        }

        public async Task CreateDb(DataBase db)
        {
            await Task.Run(() => _dataBaseServiceFactory.GetDataBaseService(db.Name, $"{_setting.RootPath[db.Settings.DefaultSource]}", db.Settings.FileSize, db.Settings.DefaultSource));
        }

        public async Task<IDataBaseService> GetDb(string dbName)
        {
            return await Task.Run(() =>
            {
                foreach(var item in _setting.RootPath)
                {
                    var dbNames = GetDbNamesBySource(item.Key);
                    if (dbNames.Contains(dbName))
                    {
                        return _dataBaseServiceFactory.GetDataBaseService(RootFormat(item.Key, dbName));
                    }
                }
                throw new ArgumentException($"Database with name: {dbName} does not exist!");
            });
        }

        private string RootFormat(SupportedSources source, string dbName)
        {
            switch (source)
            {
                case SupportedSources.Json:
                    return $"{_setting.RootPath[source]}\\{dbName}{Core.Constants.DataBaseFileExtention}";
                case SupportedSources.SqlServer:
                    return $"{_setting.RootPath[source]}{Constants.Separator}{dbName}";
                case SupportedSources.MongoDb:
                    return $"{_setting.RootPath[source]}{Constants.Separator}{dbName}";
                default:
                    throw new ArgumentException("Unsupported source!");
            }
        }

        public async Task<List<string>> GetDbNames()
        {
            return await Task.Run(() =>
            {
                var res = new List<string>();
                foreach(var key in _setting.RootPath.Keys)
                {
                    res = res.Union(GetDbNamesBySource(key)).ToList();
                }
                return res;
            });
        }
        private List<string> GetDbNamesBySource(SupportedSources source)
        {
            return _dbWriterFactory.GetDbWriter(source).GetDbsNames(_setting.RootPath[source]);
        }
    }
}
