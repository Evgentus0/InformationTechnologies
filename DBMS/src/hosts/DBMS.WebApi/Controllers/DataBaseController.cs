using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBMS.SharedModels.DTO;
using DBMS.SharedModels.ResuestHelpers;
using DBMS.WebApi.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DBMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataBaseController : ControllerBase
    {
        private IDbDal _dataBase;

        public DataBaseController(IDbDal dbDal)
        {
            _dataBase = dbDal;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<RequestResult>> CreateDb([FromBody]DataBase db)
        {
            RequestResult result = await _dataBase.CreateDb(db);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("{dbName}")]
        public async Task<ActionResult<RequestResult>> AddTable(string dbName, [FromQuery]string tableName)
        {
            RequestResult result = await _dataBase.AddTable(dbName, tableName);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete]
        [Route("{dbName}")]
        public async Task<ActionResult<RequestResult>> DeleteTable(string dbName, [FromQuery] string tableName)
        {
            RequestResult result = await _dataBase.DeleteTable(dbName, tableName);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        [Route("{dbName}")]
        public async Task<ActionResult<Table>> GetTable(string dbName, [FromQuery] string tableName)
        {
            (Table table, RequestResult result) result = await _dataBase.GetTable(dbName, tableName);

            if (result.result.IsSuccess)
            {
                return Ok(result.table);
            }
            else
            {
                return BadRequest(result.result);
            }
        }

        [HttpGet]
        [Route("{dbName}/all-tables")]
        public async Task<ActionResult<List<Table>>> GetTables(string dbName)
        {
            (List<Table> tables, RequestResult result) result = await _dataBase.GetTables(dbName);

            if (result.result.IsSuccess)
            {
                return Ok(result.tables);
            }
            else
            {
                return BadRequest(result.result);
            }
        }

        [HttpGet]
        [Route("get-db")]
        public async Task<ActionResult<DataBase>> GetDataBase([FromQuery] string dbName)
        {
            (DataBase db, RequestResult result) result = await _dataBase.GetDataBase(dbName);

            if (result.result.IsSuccess)
            {
                return Ok(result.db);
            }
            else
            {
                return BadRequest(result.result);
            }
        }

        [HttpGet]
        [Route("get-db-all")]
        public async Task<ActionResult<List<string>>> GetAllDataBase()
        {
            (List<string> dbs, RequestResult result) result = await _dataBase.GetDataBasesNames();

            if (result.result.IsSuccess)
            {
                return Ok(result.dbs);
            }
            else
            {
                return BadRequest(result.result);
            }
        }
    }
}
