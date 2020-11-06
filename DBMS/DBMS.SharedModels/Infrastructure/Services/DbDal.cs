using DBMS.SharedModels.DTO;
using DBMS.SharedModels.Infrastructure.Interfaces;
using DBMS.SharedModels.ResuestHelpers;
using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBMS.SharedModels.Infrastructure.Services
{
    public class DbDal : IDbDal
    {
        private IFileHelper _fileHelper;
        private Settings.Settings _setting;
        private IDbMapper _mapper;

        public DbDal(IFileHelper file, IDbMapper dbMapper, Settings.Settings setting)
        {
            _fileHelper = file;
            _setting = setting;
            _mapper = dbMapper;
        }

        public async Task<RequestResult> AddTable(string dbName, string tableName)
        {
            try
            {
                IDataBaseService dataBase = await _fileHelper.GetDb(dbName);
                dataBase.AddTable(tableName);

                return new RequestResult { IsSuccess = true, Message = _setting.SuccessMessage };
            }
            catch(Exception ex)
            {
                return new RequestResult { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<RequestResult> CreateDb(DataBase db)
        {
            try
            {
                await _fileHelper.CreateDb(db);

                return new RequestResult { IsSuccess = true, Message = _setting.SuccessMessage };
            }
            catch (Exception ex)
            {
                return new RequestResult { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<RequestResult> DeleteTable(string dbName, string tableName)
        {
            try
            {
                IDataBaseService dataBase = await _fileHelper.GetDb(dbName);
                dataBase.DeleteTable(tableName);

                return new RequestResult { IsSuccess = true, Message = _setting.SuccessMessage };
            }
            catch (Exception ex)
            {
                return new RequestResult { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<(DataBase db, RequestResult result)> GetDataBase(string dbName)
        {
            //try
            //{
            //    IDataBaseService dataBase = await _fileHelper.GetDb(dbName);
            //    dataBase.AddTable(tableName);

            //    return new RequestResult { IsSuccess = true, Message = _setting.SuccessMessage };
            //}
            //catch (Exception ex)
            //{
            //    return new RequestResult { IsSuccess = false, Message = ex.Message };
            //}

            throw new NotImplementedException();
        }

        public async Task<(List<string> dbs, RequestResult result)> GetDataBasesNames()
        {
            try
            {
                List<string> names = await _fileHelper.GetDbNames();

                var result = names.Select(x => x.Split('\\').Last().Split('.').First()).ToList();

                return (result, new RequestResult { IsSuccess = true, Message = _setting.SuccessMessage });
            }
            catch(Exception ex)
            {
                return (null, new RequestResult { IsSuccess = false, Message = ex.Message });
            }
        }

        public async Task<(Table table, RequestResult result)> GetTable(string dbName, string tableName)
        {
            try
            {
                IDataBaseService dataBase = await _fileHelper.GetDb(dbName);
                var table = dataBase[tableName];

                Table tablesDto = _mapper.GetDtoTables(new List<DBMS_Core.Models.Table> { table.Table }).First();

                return (tablesDto, new RequestResult { IsSuccess = true, Message = _setting.SuccessMessage });
            }
            catch (Exception ex)
            {
                return (null, new RequestResult { IsSuccess = false, Message = ex.Message });
            }
        }

        public async Task<(List<Table> tables, RequestResult result)> GetTables(string dbName)
        {
            try
            {
                IDataBaseService dataBase = await _fileHelper.GetDb(dbName);
                var tables = dataBase.GetTables().Select(x => x.Table).ToList();

                List<Table> tablesDto = _mapper.GetDtoTables(tables);

                return (tablesDto, new RequestResult { IsSuccess = true, Message = _setting.SuccessMessage });
            }
            catch (Exception ex)
            {
                return (null, new RequestResult { IsSuccess = false, Message = ex.Message });
            }
        }
    }
}
