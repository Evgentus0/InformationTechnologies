using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;
using DBMS.WebApiClient.Settings;
using System.Net.Http;
using DBMS.WebApiClient.Helpers;
using Dto = DBMS.SharedModels.DTO;
using System.Text.Json;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DBMS.SharedModels.ResuestHelpers;
using DBMS_Core.Sources;
using System.Runtime.CompilerServices;
using DBMS.SharedModels.Infrastructure.Interfaces;
using DBMS.SharedModels.Infrastructure.Helpers;

namespace DBMS.WebApiClient
{
    public class Client
    {
        private IDbMapper _mapper;

        private Settings.Settings _settings;
        private HttpClient _client;

        public Client()
        {
            _settings = new Settings.Settings();
            _client = new HttpClient();
            _mapper = new DbMapper();
        }

        public void AddTable(string dbName, string tableName)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.DbController, dbName)
                .WithParams((_settings.Constants.TableName, tableName))
                .Build();

            PostRequest(url, null);
        }

        public void DeleteTable(string dbName, string tableName)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.DbController, dbName)
                .WithParams((_settings.Constants.TableName, tableName))
                .Build();

            DeleteRequest(url);
        }

        public void CreateDb(string name, long fileSize, SupportedSources source)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.DbController)
                .Build();

            var db = new Dto.DataBase
            {
                Name = name,
                Settings = new Dto.DbSettings
                {
                    DefaultSource = source,
                    FileSize = fileSize
                }
            };

            PostRequest(url, db);
        }

        public Table GetTable(string dbName, string tableName)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.DbController, dbName)
                .WithParams((_settings.Constants.TableName, tableName))
                .Build();

            var table = GetRequest<Dto.Table>(url);

            return _mapper.FromTableDtoToTable(new List<Dto.Table> { table }).First();
        }

        public IEnumerable<Table> GetTables(string dbName)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.DbController, dbName)
                .AddUrl(_settings.Endpoints.AllTables)
                .Build();

            var tables = GetRequest<List<Dto.Table>>(url);

            return _mapper.FromTableDtoToTable(tables);
        }

        public void AddField(string dbName, string tableName, string fieldName, SupportedTypes type, List<IValidator> validators)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController, dbName, 
                    tableName, _settings.Endpoints.AddField)
                .Build();

            var field = new Dto.Field
            {
                Name = fieldName,
                Type = type,
                Validators = _mapper.GetDtoValidators(validators)
            };

            PostRequest(url, field);
        }

        public void DeleteField(string dbName, string tableName, string name)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController, dbName, 
                    tableName, _settings.Endpoints.DeleteField)
                .WithParams((_settings.Constants.FieldName, name))
                .Build();

            DeleteRequest(url);
        }

        public void DeleteRows(string dbName, string tableName, Dictionary<string, List<IValidator>> conditions)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController, dbName, tableName, 
                    _settings.Endpoints.Data, _settings.Endpoints.DeleteConditions)
                .Build();

            var conditionsDto = new Dictionary<string, List<Dto.Validator>>();
            foreach (var item in conditions)
            {
                conditionsDto.Add(item.Key, _mapper.GetDtoValidators(item.Value));
            }

            PostRequest(url, conditionsDto);
        }

        public void DeleteRows(string dbName, string tableName, List<Guid> ids)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController, dbName, tableName, 
                    _settings.Endpoints.Data, _settings.Endpoints.DeleteIds)
                .Build();


            PostRequest(url, ids);
        }

        public void InsertData(string dbName, string tableName, List<List<object>> lists)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController, 
                    dbName, tableName, _settings.Endpoints.Data)
                .Build();


            PostRequest(url, lists);
        }

        public List<List<object>> Select(string dbName, string tableName)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController, dbName, tableName, 
                    _settings.Endpoints.Data, _settings.Endpoints.Select)
                .Build();

            var selectRequest = new SelectRequest
            {
                Offset = 0,
                Top = 100
            };

            var data = PostRequest<List<List<object>>>(url, selectRequest);

            return data;
        }

        public List<List<object>> Select(string dbName, string tableName, int top, int offset)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController, dbName, 
                    tableName, _settings.Endpoints.Data, _settings.Endpoints.Select)
                .Build();

            var selectRequest = new SelectRequest
            {
                Offset = offset,
                Top = top
            };

            var data = PostRequest<List<List<object>>>(url, selectRequest);

            return data;
        }

        public List<List<object>> Select(string dbName, string tableName, Dictionary<string, List<IValidator>> conditions)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController, dbName, tableName, 
                    _settings.Endpoints.Data, _settings.Endpoints.Select)
                .Build();

            var conditionsDto = new Dictionary<string, List<Dto.Validator>>();
            foreach (var item in conditions)
            {
                conditionsDto.Add(item.Key, _mapper.GetDtoValidators(item.Value));
            }

            var selectRequest = new SelectRequest
            {
                Offset = 0,
                Top = 100,
                Conditions = conditionsDto
            };

            var data = PostRequest<List<List<object>>>(url, selectRequest);

            return data;
        }

        public List<List<object>> Select(string dbName, string tableName, int top, int offset, Dictionary<string, List<IValidator>> conditions)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController, dbName, tableName, 
                    _settings.Endpoints.Data, _settings.Endpoints.Select)
                .Build();

            var conditionsDto = new Dictionary<string, List<Dto.Validator>>();
            foreach (var item in conditions)
            {
                conditionsDto.Add(item.Key, _mapper.GetDtoValidators(item.Value));
            }

            var selectRequest = new SelectRequest
            {
                Offset = offset,
                Top = top,
                Conditions = conditionsDto
            };

            var data = PostRequest<List<List<object>>>(url, selectRequest);

            return data;
        }

        public List<List<object>> Union(string dbName, string tableName, Table[] tables)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController, dbName, tableName, 
                    _settings.Endpoints.Data, _settings.Endpoints.Union)
                .Build();

            var list = tables.Select(x => x.Name);

            var data = PostRequest<List<List<object>>>(url, list);

            return data;
        }

        public void UpdateRows(string dbName, string tableName, List<List<object>> rows)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController, 
                    dbName, tableName, _settings.Endpoints.Data)
                .Build();


            PutRequest(url, rows);
        }

        public void UpdateSchema(string dbName, Table table)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController, dbName, 
                    table.Name, _settings.Endpoints.UpdateSchema)
                .Build();

            var tableDto = new Dto.Table
            {
                Name = table.Name,
                TableSchema = new Dto.TableSchema
                {
                    Fields = table.Schema.Fields.Select(x => new Dto.Field
                    {
                        Name = x.Name,
                        Type = x.Type
                    }).ToList()
                }
            };

            PostRequest(url, tableDto);
        }

        public List<string> GetDbsList()
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.DbController, _settings.Endpoints.GetAllDb)
                .Build();

            var dbsList = GetRequest<List<string>>(url);

            return dbsList;
        }

        private T GetRequest<T>(string url)
        {
            var response = _client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<T>(response.Content.ReadAsStringAsync().Result);

            return result;
        }

        private T PostRequest<T>(string url, object data)
        {
            HttpContent content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            var response = _client.PostAsync(url, content).Result;
            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<T>(response.Content.ReadAsStringAsync().Result);
            return result;
        }

        private void PostRequest(string url, object data)
        {
            HttpContent content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            var response = _client.PostAsync(url, content).Result;
            response.EnsureSuccessStatusCode();
        }

        private T PutRequest<T>(string url, object data, bool returnValue = false)
        {
            HttpContent content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            var response = _client.PutAsync(url, content).Result;
            response.EnsureSuccessStatusCode();

            if (returnValue)
            {
                var result = JsonSerializer.Deserialize<T>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
            return default(T);
        }

        private void PutRequest(string url, object data)
        {
            HttpContent content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            var response = _client.PutAsync(url, content).Result;
            response.EnsureSuccessStatusCode();
        }

        private void DeleteRequest(string url)
        {
            var response = _client.DeleteAsync(url).Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
