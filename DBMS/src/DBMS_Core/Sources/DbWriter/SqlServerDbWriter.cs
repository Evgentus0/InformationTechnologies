using DBMS.SqlServerSource;
using DBMS.SqlServerSource.Clients;
using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DBMS_Core.Sources.DbWriter
{
    class SqlServerDbWriter : IDbWriter
    {
        public void DeleteDb(DataBase dataBase)
        {
            var client = DbClientFactory.GetClient(dataBase.Settings.RootPath, dataBase.Name);
            client.DeleteDatabase();
        }

        public DataBase GetDb(string filePath)
        {
            var data = filePath.Split(Constants.Separator);
            var client = DbClientFactory.GetClient(data[0], data[1]);

            string dbString = client.GetDb();
            return JsonSerializer.Deserialize<DataBase>(dbString);
        }

        public List<string> GetDbsNames(string rootPath)
        {
            var client = DbClientFactory.GetClient(rootPath, string.Empty);
            return client.GetDbsNames();
        }

        public void UpdateDb(DataBase dataBase)
        {
            var client = DbClientFactory.GetClient(dataBase.Settings.RootPath, dataBase.Name);
            string stringData = JsonSerializer.Serialize(dataBase);
            client.UpdateOrCreateDb(stringData);
        }
    }
}
