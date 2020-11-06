using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBMS.SharedModels.Infrastructure.Interfaces;
using DBMS.SharedModels.Infrastructure.Services;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace DBMS.GrpcServer
{
    public class DatabaseService : DBService.DBServiceBase
    {
        private IDbDal databaseService;
        public override Task<BaseReply> CreateDatabase(CreateDbRequest request, ServerCallContext context)
        {
            try
            {
                

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Database created: " + request.Name);
                Console.WriteLine("***************************************************************************************************************");

                return Task.FromResult(new BaseReply() { Code = 200, Message = "", StackTrace = "" });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new BaseReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        public override Task<BaseReply> CreateTable(TableRequest request, ServerCallContext context)
        {
            try
            {
                databaseService.AddTable(request.DbName ,request.TableName);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Table created: " + request.TableName);
                Console.WriteLine("***************************************************************************************************************");

                return Task.FromResult(new BaseReply() { Code = 200 });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new BaseReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        public override Task<BaseReply> DeleteTable(TableRequest request, ServerCallContext context)
        {

            try
            {
                
                databaseService.DeleteTable(request.DbName, request.TableName);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Table deleted: " + request.TableName);
                Console.WriteLine("***************************************************************************************************************");

                return Task.FromResult(new BaseReply() { Code = 200 });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new BaseReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        
    }
}
