using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace DBMS_Core.Sources.DbWriter
{
    class JsonDbWriter : IDbWriter
    {
        public DataBase GetDb(string filePath)
        {
            string data = File.ReadAllText(filePath);

            var dataBase = JsonSerializer.Deserialize<DataBase>(data);
            return dataBase;
        }

        public void UpdateDb(DataBase dataBase)
        {
            var stringData = JsonSerializer.Serialize(dataBase);

            File.WriteAllText($"{dataBase.Settings.RootPath}\\{dataBase.Name}{Constants.DataBaseFileExtention}", stringData);
        }

    }
}
