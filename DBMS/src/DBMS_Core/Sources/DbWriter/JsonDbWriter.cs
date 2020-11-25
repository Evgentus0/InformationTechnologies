using DBMS.SqlServerSource.Interfaces;
using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace DBMS_Core.Sources.DbWriter
{
    public class JsonDbWriter : IDbWriter
    {
        private IDbClientFactory _dbClientFactory;

        public JsonDbWriter(IDbClientFactory dbClientFactory)
        {
            _dbClientFactory = dbClientFactory;
        }

        public void DeleteDb(DataBase dataBase)
        {
            File.Delete($"{dataBase.Settings.RootPath}\\{dataBase.Name}{Constants.DataBaseFileExtention}");
        }

        public DataBase GetDb(string filePath)
        {
            string data = File.ReadAllText(filePath);

            var dataBase = JsonSerializer.Deserialize<DataBase>(data);
            return dataBase;
        }

        public List<string> GetDbsNames(string rootPath)
        {
            var rootList = Directory.GetFiles(rootPath, $"*{Constants.DataBaseFileExtention}").ToList();
            var result = rootList.Select(x =>
            {
                var nameWithExtention = x.Split('\\').Last();
                var name = nameWithExtention.Split('.').First();

                return name;
            });

            return result.ToList();
        }

        public void UpdateDb(DataBase dataBase)
        {
            var stringData = JsonSerializer.Serialize(dataBase);

            File.WriteAllText($"{dataBase.Settings.RootPath}\\{dataBase.Name}{Constants.DataBaseFileExtention}", stringData);
        }

    }
}
