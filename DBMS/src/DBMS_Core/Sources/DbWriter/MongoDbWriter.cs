using DBMS.SqlServerSource;
using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DBMS_Core.Sources.DbWriter
{
    class MongoDbWriter : IDbWriter
    {
        public void DeleteDb(DataBase dataBase)
        {
            var client = DbClientFactory.GetMongoClient(dataBase.Settings.RootPath, dataBase.Name);
            client.DeleteDatabase();
        }

        public DataBase GetDb(string filePath)
        {
            var data = filePath.Split(Constants.Separator);
            var client = DbClientFactory.GetMongoClient(data[0], data[1]);

            string dbString = client.GetDb();
            return JsonSerializer.Deserialize<DataBase>(dbString);
        }

        public List<string> GetDbsNames(string rootPath)
        {
            var client = DbClientFactory.GetMongoClient(rootPath, string.Empty);
            return client.GetDbsNames();
        }

        public void UpdateDb(DataBase dataBase)
        {
            var client = DbClientFactory.GetMongoClient(dataBase.Settings.RootPath, dataBase.Name);
            string stringData = JsonSerializer.Serialize(dataBase);
            client.UpdateOrCreateDb(stringData);
        }
    }
}
