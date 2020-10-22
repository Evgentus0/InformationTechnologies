using DBMS.SharedModels.DTO;
using DBMS.SharedModels.ResuestHelpers;
using DBMS.WebApi.Infrastructure.Interfaces;
using DBMS.WebApi.Settings;
using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DBMS.WebApi.Infrastructure.Services
{
    public class TableDal : ITableDal
    {
        private IFileHelper _fileHelper;
        private IDbMapper _dbMapper;
        private Settings.Settings _setting;

        public TableDal(IFileHelper file, Settings.Settings setting, IDbMapper dbMapper)
        {
            _fileHelper = file;
            _setting = setting;
            _dbMapper = dbMapper;
        }

        public async Task<RequestResult> AddField(string tableName, string dbName, Field field)
        {
            try
            {
                var db = await _fileHelper.GetDb(dbName);
                var table = db[tableName];

                table.AddNewField(field.Name, field.Type, _dbMapper.GetValidators(field.Validators));

                return new RequestResult { IsSuccess = true, Message = _setting.SuccessMessage };
            }
            catch(Exception ex)
            {
                return new RequestResult { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<RequestResult> Delete(string tableName, string dbName, Dictionary<string, List<Validator>> conditions)
        {
            try
            {
                var db = await _fileHelper.GetDb(dbName);
                var table = db[tableName];
                var dbConditions = new Dictionary<string, List<IValidator>>();

                foreach (var item in conditions)
                {
                    dbConditions.Add(item.Key, _dbMapper.GetValidators(item.Value));
                }

                table.DeleteRows(dbConditions);

                return new RequestResult { IsSuccess = true, Message = _setting.SuccessMessage };
            }
            catch (Exception ex)
            {
                return new RequestResult { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<RequestResult> Delete(string tableName, string dbName, List<Guid> ids)
        {
            try
            {
                var db = await _fileHelper.GetDb(dbName);
                var table = db[tableName];

                table.DeleteRows(ids);

                return new RequestResult { IsSuccess = true, Message = _setting.SuccessMessage };
            }
            catch (Exception ex)
            {
                return new RequestResult { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<RequestResult> DeleteField(string tableName, string dbName, string fieldName)
        {
            try
            {
                var db = await _fileHelper.GetDb(dbName);
                var table = db[tableName];

                table.DeleteField(fieldName);

                return new RequestResult { IsSuccess = true, Message = _setting.SuccessMessage };
            }
            catch (Exception ex)
            {
                return new RequestResult { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<RequestResult> InsertData(string tableName, string dbName, List<List<object>> data)
        {
            try
            {
                var db = await _fileHelper.GetDb(dbName);
                var table = db[tableName];

                table.InsertDataRange(data);

                return new RequestResult { IsSuccess = true, Message = _setting.SuccessMessage };
            }
            catch (Exception ex)
            {
                return new RequestResult { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<(List<List<object>> data, RequestResult result)> Select(string tableName, string dbName, SelectRequest requestParameters)
        {
            try
            {
                var db = await _fileHelper.GetDb(dbName);
                var table = db[tableName];
                var data = new List<List<object>>();

                if(requestParameters.Conditions != null)
                {
                    var dbConditions = new Dictionary<string, List<IValidator>>();

                    foreach (var item in requestParameters.Conditions)
                    {
                        dbConditions.Add(item.Key, _dbMapper.GetValidators(item.Value));
                    }

                    data = table.Select(requestParameters.Top, requestParameters.Offset, dbConditions);
                }
                else
                {
                    data = table.Select(requestParameters.Top, requestParameters.Offset);
                }

                return (data, new RequestResult { IsSuccess = true, Message = _setting.SuccessMessage });
            }
            catch (Exception ex)
            {
                return (null, new RequestResult { IsSuccess = false, Message = ex.Message });
            }
        }

        public async Task<(List<List<object>> data, RequestResult result)> Union(string tableName, string dbName, List<string> tableNames)
        {
            try
            {
                var db = await _fileHelper.GetDb(dbName);
                var table = db[tableName];
                var data = new List<List<object>>();
                var tables = tableNames.Select(x => db[x]);

                data = table.Union(tables.Select(x => x.Table).ToArray());

                return (data, new RequestResult { IsSuccess = true, Message = _setting.SuccessMessage });
            }
            catch (Exception ex)
            {
                return (null, new RequestResult { IsSuccess = true, Message = ex.Message });
            }
        }

        public async Task<RequestResult> UpdateData(string tableName, string dbName, List<List<object>> data)
        {
            try
            {
                var db = await _fileHelper.GetDb(dbName);
                var table = db[tableName];

                table.UpdateRows(data);

                return new RequestResult { IsSuccess = true, Message = _setting.SuccessMessage };
            }
            catch (Exception ex)
            {
                return new RequestResult { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<RequestResult> UpdateSchema(string tableName, string dbName, Table table)
        {
            try
            {               
                return new RequestResult { IsSuccess = true, Message = _setting.SuccessMessage };
            }
            catch (Exception ex)
            {
                return new RequestResult { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}
