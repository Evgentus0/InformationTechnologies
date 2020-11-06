using DBMS.SqlServerSource.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DBMS.SqlServerSource.Clients
{
    class JsonDbClient : IDbClient
    {
        private string _path;
        public JsonDbClient(string path)
        {
            _path = path;
        }

        public void ClearTable()
        {
            File.WriteAllText(_path, string.Empty);
        }

        public void CreateTable()
        {
            using (File.Create(_path)) { }
        }



        public void DeleteTable()
        {
            File.Delete(_path);
        }

        public List<string> GetData()
        {
            var data = File.ReadAllText(_path);
            if (string.IsNullOrEmpty(data))
            {
                return new List<string>();
            }

            var result = JsonSerializer.Deserialize<List<string>>(data);

            return result;
        }

        public async Task<List<string>> GetDataAsync()
        {
            using (var data = File.OpenRead(_path))
            {
                var result = await JsonSerializer.DeserializeAsync<List<string>>(data);

                return result;
            }
        }

        public void InsertData(List<string> data)
        {
            if (!(data == null || data.Count == 0))
            {
                var newStringData = JsonSerializer.Serialize(data);

                File.WriteAllText(_path, newStringData);
            }
        }

        public async Task InsertDataAsync(List<string> data)
        {
            if (!(data == null || data.Count == 0))
            {
                using (var streamData = File.OpenWrite(_path))
                {
                    await JsonSerializer.SerializeAsync(streamData, data);
                }
            }
        }

        public void DeleteDatabase()
        {
            throw new NotImplementedException();
        }

        public void UpdateOrCreateDb(string stringDbData)
        {
            throw new NotImplementedException();
        }
        public string GetDb()
        {
            throw new NotImplementedException();
        }

        public List<string> GetDbsNames()
        {
            throw new NotImplementedException();
        }
    }
}
