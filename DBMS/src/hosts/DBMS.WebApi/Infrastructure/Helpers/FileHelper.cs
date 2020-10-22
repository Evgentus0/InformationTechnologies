using DBMS.SharedModels.DTO;
using DBMS.WebApi.Infrastructure.Interfaces;
using DBMS.WebApi.Settings;
using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core = DBMS_Core;

namespace DBMS.WebApi.Infrastructure.Helpers
{
    public class FileHelper : IFileHelper
    {
        private Settings.Settings _setting;
        public FileHelper(Settings.Settings setting)
        {
            _setting = setting;
        }

        public async Task CreateDb(DataBase db)
        {
            await Task.Run(() => DataBaseServiceFactory.GetDataBaseService(db.Name, $"{_setting.RootPath}", db.Settings.FileSize, db.Settings.DefaultSource));
        }

        public async Task<IDataBaseService> GetDb(string dbName)
        {
            return await Task.Run(() => DataBaseServiceFactory.GetDataBaseService($"{_setting.RootPath}\\{dbName}{Core.Constants.DataBaseFileExtention}"));
        }

        public async Task<List<string>> GetDbNames()
        {
            return await Task.Run(() => Directory.GetFiles(_setting.RootPath, $"*{Core.Constants.DataBaseFileExtention}").ToList());
        }
    }
}
