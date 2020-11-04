using DBMS.SqlServerSource.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.SqlServerSource.Clients
{
    public class MongoDbClient : IDbClient
    {
        private string _connectionString;
        private string _dbName;
        private string _tableName;
        private string _dbTableName;

        private const string DATA = "Data";

        MongoClient _mongoClient;
        IMongoDatabase _dataBase;

        public MongoDbClient(string connectionString, string dbName, string tableName = "")
        {
            _connectionString = connectionString;
            _dbName = dbName;
            _tableName = tableName;
            _dbTableName = $"db_{_dbName}_name";

            _mongoClient = new MongoClient(_connectionString);
            _dataBase = _mongoClient.GetDatabase(dbName);
        }

        public void ClearTable()
        {
            _dataBase.DropCollection(_tableName);
            CreateTable();
        }

        public void CreateTable()
        {
            _dataBase.CreateCollection(_tableName);
            var collection = _dataBase.GetCollection<BsonDocument>(_tableName);
            collection.InsertOne(new BsonDocument
            {
                {DATA, new BsonArray() }
            });
        }

        public void DeleteTable()
        {
            _dataBase.DropCollection(_tableName);
        }

        public List<string> GetData()
        {
            var collection = _dataBase.GetCollection<DbData>(_tableName);

            var data = collection.Find(new BsonDocument()).First();

            return data.Data;
        }

        public async Task<List<string>> GetDataAsync()
        {
            var collection = _dataBase.GetCollection<string>(_tableName);

            var data = (await collection.FindAsync(new BsonDocument())).ToList();
            return data;
        }

        public string GetDb()
        {
            var collection = _dataBase.GetCollection<DbDataBaseInfo>(_dbTableName);
            var data = collection.Find(new BsonDocument()).First();
            return data.Data;
        }

        public void InsertData(List<string> data)
        {
            var collection = _dataBase.GetCollection<BsonDocument>(_tableName);
            var update = Builders<BsonDocument>.Update.PushEach(DATA, data);
            collection.UpdateMany(new BsonDocument(), update);
        }

        public async Task InsertDataAsync(List<string> data)
        {
            var collection = _dataBase.GetCollection<BsonDocument>(_tableName);
            var update = Builders<BsonDocument>.Update.Push(DATA, data);
            await collection.UpdateManyAsync(default, update);
        }

        public void UpdateOrCreateDb(string stringDbData)
        {
            try
            {
                _dataBase.DropCollection(_dbTableName);
            }
            finally
            {
                _dataBase.CreateCollection(_dbTableName);
                var collection = _dataBase.GetCollection<BsonDocument>(_dbTableName);

                collection.InsertOne(new BsonDocument
                {
                    {DATA, stringDbData }
                });
            }
        }

        public void DeleteDatabase()
        {
            try
            {
                _mongoClient.DropDatabase(_dbName);
            }
            catch { }
        }

        private class DbData
        {
            public object _id { get; set; }
            public List<string> Data { get; set; }
        }

        private class DbDataBaseInfo
        {
            public object _id { get; set; }
            public string Data { get; set; }
        }
    }
}
