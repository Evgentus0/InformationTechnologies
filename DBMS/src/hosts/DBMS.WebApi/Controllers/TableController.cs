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
    [Route("api/[controller]/{dbName}")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private ITableDal _tableService;

        public TableController(ITableDal tableDal)
        {
            _tableService = tableDal;
        }

        [HttpPost]
        [Route("{tableName}/add-field")]
        public async Task<ActionResult<RequestResult>> AddField(string tableName, string dbName, [FromBody] Field field)
        {
            RequestResult result = await _tableService.AddField(tableName, dbName, field);

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
        [Route("{tableName}/delete-field")]
        public async Task<ActionResult<RequestResult>> DeleteField(string tableName, string dbName, [FromQuery] string fieldName)
        {
            RequestResult result = await _tableService.DeleteField(tableName, dbName, fieldName);

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
        [Route("{tableName}/data")]
        public async Task<ActionResult<RequestResult>> InsertData(string tableName, string dbName, [FromBody]List<List<object>> data)
        {
            RequestResult result = await _tableService.InsertData(tableName, dbName, data);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut]
        [Route("{tableName}/data")]
        public async Task<ActionResult> UpdateData(string tableName, string dbName, [FromBody] List<List<object>> data)
        {
            RequestResult result = await _tableService.UpdateData(tableName, dbName, data);

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
        [Route("{tableName}/data/delete-conditions")]
        public async Task<ActionResult> DeleteData(string tableName, string dbName, [FromBody] Dictionary<string, List<Validator>> conditions)
        {
            RequestResult result = await _tableService.Delete(tableName, dbName, conditions);

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
        [Route("{tableName}/data/delete-ids")]
        public async Task<ActionResult> DeleteData(string tableName, string dbName, [FromBody] List<Guid> ids)
        {
            RequestResult result = await _tableService.Delete(tableName, dbName, ids);

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
        [Route("{tableName}/update-schema")]
        public async Task<ActionResult> UpdateSchema(string tableName, string dbName, [FromBody] Table table)
        {
            RequestResult result = await _tableService.UpdateSchema(tableName, dbName, table);

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
        [Route("{tableName}/data/select")]
        public async Task<ActionResult<List<List<object>>>> Select(string tableName, string dbName, [FromBody] SelectRequest requestParameters)
        {
            (List<List<object>> data, RequestResult result) result = await _tableService.Select(tableName, dbName, requestParameters);

            if (result.result.IsSuccess)
            {
                return Ok(result.data);
            }
            else
            {
                return BadRequest(result.result);
            }
        }

        [HttpPost]
        [Route("{tableName}/data/union")]
        public async Task<ActionResult<List<List<object>>>> Union(string tableName, string dbName, [FromBody] List<string> tableNames)
        {
            (List<List<object>> data, RequestResult result) result = await _tableService.Union(tableName, dbName, tableNames);

            if (result.result.IsSuccess)
            {
                return Ok(result.data);
            }
            else
            {
                return BadRequest(result.result);
            }
        }
    }
}
