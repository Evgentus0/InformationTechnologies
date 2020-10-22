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

namespace DBMS.WebApiClient
{
    public class Client
    {
        private Mapper _mapper;

        private Settings.Settings _settings;
        private HttpClient _client;

        public Client()
        {
            _settings = new Settings.Settings();
            _client = new HttpClient();
            _mapper = new Mapper();
        }

        public void AddTable(string dbName, string tableName)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.DbController)
                .AddUrl(dbName)
                .WithParams((_settings.Constants.TableName, tableName))
                .Build();

            _client.PostAsync(url, null);
        }

        public void DeleteTable(string dbName, string tableName)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.DbController)
                .AddUrl(dbName)
                .WithParams((_settings.Constants.TableName, tableName))
                .Build();

            _client.DeleteAsync(url);
        }

        public Table GetTable(string dbName, string tableName)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.DbController)
                .AddUrl(dbName)
                .WithParams((_settings.Constants.TableName, tableName))
                .Build();

            var response = _client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            var table = JsonSerializer.Deserialize<Dto.Table>(response.Content.ReadAsStringAsync().Result);

            return _mapper.FromTableDtoToTable(table);
        }

        public IEnumerable<Table> GetTables(string dbName)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.DbController)
                .AddUrl(dbName)
                .AddUrl(_settings.Endpoints.AllTables)
                .Build();

            var response = _client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            var tables = JsonSerializer.Deserialize<List<Dto.Table>>(response.Content.ReadAsStringAsync().Result);

            return _mapper.FromTableDtoToTable(tables);
        }

        public void AddField(string dbName, string tableName, string fieldName, SupportedTypes type, List<IValidator> validators)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController)
                .AddUrl(dbName)
                .AddUrl(tableName)
                .AddUrl(_settings.Endpoints.AddField)
                .Build();

            var field = new Dto.Field
            {
                Name = fieldName,
                Type = type,
                Validators = _mapper.GetDtoValidators(validators)
            };

            HttpContent content = new StringContent(JsonSerializer.Serialize(field));

            var response = _client.PostAsync(url, content).Result;
            response.EnsureSuccessStatusCode();
        }

        public void DeleteField(string dbName, string tableName, string name)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController)
                .AddUrl(dbName)
                .AddUrl(tableName)
                .AddUrl(_settings.Endpoints.DeleteField)
                .Build();

            var response = _client.DeleteAsync(url).Result;
            response.EnsureSuccessStatusCode();
        }

        public void DeleteRows(string dbName, string tableName, Dictionary<string, List<IValidator>> conditions)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController)
                .AddUrl(dbName)
                .AddUrl(tableName)
                .AddUrl(_settings.Endpoints.Data)
                .AddUrl(_settings.Endpoints.DeleteConditions)
                .Build();

            var conditionsDto = new Dictionary<string, List<Dto.Validator>>();
            foreach (var item in conditions)
            {
                conditionsDto.Add(item.Key, _mapper.GetDtoValidators(item.Value));
            }

            HttpContent content = new StringContent(JsonSerializer.Serialize(conditionsDto));

            var response = _client.PostAsync(url, content).Result;
            response.EnsureSuccessStatusCode();
        }

        public void DeleteRows(string dbName, string tableName, List<Guid> ids)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController)
                .AddUrl(dbName)
                .AddUrl(tableName)
                .AddUrl(_settings.Endpoints.Data)
                .AddUrl(_settings.Endpoints.DeleteIds)
                .Build();


            HttpContent content = new StringContent(JsonSerializer.Serialize(ids));

            var response = _client.PostAsync(url, content).Result;
            response.EnsureSuccessStatusCode();
        }

        public void InsertData(string dbName, string tableName, List<List<object>> lists)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController)
                .AddUrl(dbName)
                .AddUrl(tableName)
                .AddUrl(_settings.Endpoints.Data)
                .Build();


            HttpContent content = new StringContent(JsonSerializer.Serialize(lists));

            var response = _client.PostAsync(url, content).Result;
            response.EnsureSuccessStatusCode();
        }

        public List<List<object>> Select(string dbName, string tableName)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController)
                .AddUrl(dbName)
                .AddUrl(tableName)
                .AddUrl(_settings.Endpoints.Data)
                .AddUrl(_settings.Endpoints.Select)
                .Build();

            var selectRequest = new SelectRequest
            {
                Offset = 0,
                Top = 100
            };

            HttpContent content = new StringContent(JsonSerializer.Serialize(selectRequest));

            var response = _client.PostAsync(url, content).Result;
            response.EnsureSuccessStatusCode();

            var data = JsonSerializer.Deserialize<List<List<object>>>(response.Content.ReadAsStringAsync().Result);

            return data;
        }

        public List<List<object>> Select(string dbName, string tableName, int top, int offset)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController)
                .AddUrl(dbName)
                .AddUrl(tableName)
                .AddUrl(_settings.Endpoints.Data)
                .AddUrl(_settings.Endpoints.Select)
                .Build();

            var selectRequest = new SelectRequest
            {
                Offset = offset,
                Top = top
            };

            HttpContent content = new StringContent(JsonSerializer.Serialize(selectRequest));

            var response = _client.PostAsync(url, content).Result;
            response.EnsureSuccessStatusCode();

            var data = JsonSerializer.Deserialize<List<List<object>>>(response.Content.ReadAsStringAsync().Result);

            return data;
        }

        public List<List<object>> Select(string dbName, string tableName, Dictionary<string, List<IValidator>> conditions)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController)
                .AddUrl(dbName)
                .AddUrl(tableName)
                .AddUrl(_settings.Endpoints.Data)
                .AddUrl(_settings.Endpoints.Select)
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

            HttpContent content = new StringContent(JsonSerializer.Serialize(selectRequest));

            var response = _client.PostAsync(url, content).Result;
            response.EnsureSuccessStatusCode();

            var data = JsonSerializer.Deserialize<List<List<object>>>(response.Content.ReadAsStringAsync().Result);

            return data;
        }

        public List<List<object>> Select(string dbName, string tableName, int top, int offset, Dictionary<string, List<IValidator>> conditions)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController)
                .AddUrl(dbName)
                .AddUrl(tableName)
                .AddUrl(_settings.Endpoints.Data)
                .AddUrl(_settings.Endpoints.Select)
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

            HttpContent content = new StringContent(JsonSerializer.Serialize(selectRequest));

            var response = _client.PostAsync(url, content).Result;
            response.EnsureSuccessStatusCode();

            var data = JsonSerializer.Deserialize<List<List<object>>>(response.Content.ReadAsStringAsync().Result);

            return data;
        }

        public List<List<object>> Union(string dbName, string tableName, Table[] tables)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController)
                .AddUrl(dbName)
                .AddUrl(tableName)
                .AddUrl(_settings.Endpoints.Data)
                .AddUrl(_settings.Endpoints.Union)
                .Build();

            var list = tables.Select(x => x.Name);

            HttpContent content = new StringContent(JsonSerializer.Serialize(list));

            var response = _client.PostAsync(url, content).Result;
            response.EnsureSuccessStatusCode();

            var data = JsonSerializer.Deserialize<List<List<object>>>(response.Content.ReadAsStringAsync().Result);

            return data;
        }

        public void UpdateRows(string dbName, string tableName, List<List<object>> rows)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController)
                .AddUrl(dbName)
                .AddUrl(tableName)
                .AddUrl(_settings.Endpoints.Data)
                .Build();


            HttpContent content = new StringContent(JsonSerializer.Serialize(rows));

            var response = _client.PutAsync(url, content).Result;
            response.EnsureSuccessStatusCode();
        }

        public void UpdateSchema(string dbName, Table table)
        {
            var url = RequestBuilder.StartBuild(_settings.Host)
                .AddUrl(_settings.Constants.TableController)
                .AddUrl(dbName)
                .AddUrl(table.Name)
                .AddUrl(_settings.Endpoints.UpdateSchema)
                .Build();


            HttpContent content = new StringContent(JsonSerializer.Serialize(table));

            var response = _client.PostAsync(url, content).Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
