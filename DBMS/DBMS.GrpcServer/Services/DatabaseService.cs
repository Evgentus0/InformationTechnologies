using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBMS.GrpcServer.Helpers;
using DBMS.SharedModels.Infrastructure.Interfaces;
using DBMS.SharedModels.Infrastructure.Services;
using DBMS_Core.Sources;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Dto = DBMS.SharedModels.DTO;

namespace DBMS.GrpcServer.Services
{
    public class DatabaseService : GrpcDBService.GrpcDBServiceBase
    {
        private IDbDal _databaseService;
        private IGrpcModelMapper _grpcModelMapper;

        public DatabaseService(
            IDbDal databaseService,
            IGrpcModelMapper grpcModelMapper)
        {
            _databaseService = databaseService;
            _grpcModelMapper = grpcModelMapper;
        }

        public override async Task<BaseReply> CreateDatabase(CreateDbRequest request, ServerCallContext context)
        {
            try
            {
                var db = new Dto.DataBase
                {
                    Name = request.Name,
                    Settings = new Dto.DbSettings
                    {
                        FileSize = request.FileSize,
                        DefaultSource = (SupportedSources)request.SourceType
                    }
                };

                await _databaseService.CreateDb(db);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Database created: " + request.Name);
                Console.WriteLine("***************************************************************************************************************");

                return new BaseReply() { Code = 200, Message = "", StackTrace = "" };
            }
            catch (Exception ex)
            {
                return new BaseReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace };
            }
        }

        public override async Task<BaseReply> CreateTable(TableRequest request, ServerCallContext context)
        {
            try
            {
                await _databaseService.AddTable(request.DbName ,request.TableName);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Table created: " + request.TableName);
                Console.WriteLine("***************************************************************************************************************");

                return new BaseReply() { Code = 200 };
            }
            catch (Exception ex)
            {
                return new BaseReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace };
            }
        }

        public override async Task<BaseReply> DeleteTable(TableRequest request, ServerCallContext context)
        {
            try
            {
                await _databaseService.DeleteTable(request.DbName, request.TableName);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Table deleted: " + request.TableName);
                Console.WriteLine("***************************************************************************************************************");

                return new BaseReply() { Code = 200 };
            }
            catch (Exception ex)
            {
                return new BaseReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace };
            }
        }

        public override async Task<GetEntityReply> GetTable(GetEntityRequest request, ServerCallContext context)
        {
            try
            {
                var table = await _databaseService.GetTable(request.DbName, request.TableName);
                GetEntityReply response = _grpcModelMapper.GetGetEntityReplyFromTableDto(table.table);
                response.Code = 200;

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Get table: " + request.TableName);
                Console.WriteLine("***************************************************************************************************************");

                return response;
            }
            catch (Exception ex)
            {
                
                return new GetEntityReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace };
            }
        }

        public override async Task<GetTableListReply> GetTableList(GetTableListRequest request, ServerCallContext context)
        {
            try
            {
                var tables = await _databaseService.GetTables(request.DbName);
                GetTableListReply response = new GetTableListReply
                {
                    Code = 200,
                };
                response.Tables.AddRange(tables.tables.Select(x => x.Name));

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Get tables from db:  " + request.DbName);
                Console.WriteLine("***************************************************************************************************************");

                return response;
            }
            catch (Exception ex)
            {

                return new GetTableListReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace };
            }
        }
    }
}
