using DBMS.GrpcServer.Helpers;
using DBMS.SharedModels.Infrastructure.Interfaces;
using DBMS_Core.Models.Types;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dto = DBMS.SharedModels.DTO;
using RequestHelper = DBMS.SharedModels.ResuestHelpers;

namespace DBMS.GrpcServer.Services
{
    public class TableService : GrpcTableService.GrpcTableServiceBase
    {
        private ITableDal _tableDal;
        private IGrpcModelMapper _grpcModelMapper;

        public TableService(
            ITableDal tableDal,
            IGrpcModelMapper grpcModelMapper)
        {
            _tableDal = tableDal;
            _grpcModelMapper = grpcModelMapper;
        }

        public override async Task<BaseReply> AddColumn(AddColumnRequest request, ServerCallContext context)
        {
            try
            {
                var field = new Dto.Field
                {
                    Name = request.ColumnName,
                    Type = (SupportedTypes)request.DataValueType,
                    Validators = _grpcModelMapper.GetValidatorsDtoFromGrpcValidators(request.Validators)
                };

                await _tableDal.AddField(request.TableName, request.DbName, field);

                var response = new BaseReply()
                {
                    Code = 200
                };

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Column added: " + request.TableName + ", " + request.ColumnName);
                Console.WriteLine("***************************************************************************************************************");

                return response;
            }
            catch (Exception ex)
            {
                return new BaseReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace };
            }
        }

        public override async Task<BaseReply> Delete(DeleteRequest request, ServerCallContext context)
        {
            try
            {
                if (request.Guids.Count > 0)
                {
                    var guids = request.Guids.Select(x => Guid.Parse(x)).ToList();

                    await _tableDal.Delete(request.TableName, request.DbName, guids);
                }

                var response = new BaseReply()
                {
                    Code = 200
                };

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Rows deleted:" + request.TableName);
                Console.WriteLine("***************************************************************************************************************");

                return response;
            }
            catch (Exception ex)
            {
                return new BaseReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace };
            }
        }

        public override async Task<BaseReply> DropColumn(DropColumnRequst request, ServerCallContext context)
        {
            try
            {
                await _tableDal.DeleteField(request.TableName, request.DbName, request.ColumnName);
                var response = new BaseReply()
                {
                    Code = 200
                };

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Column dropped: " + request.TableName + ", " + request.ColumnName);
                Console.WriteLine("***************************************************************************************************************");

                return response;
            }
            catch (Exception ex)
            {
                return new BaseReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace };
            }
        }

        public override async Task<BaseReply> Insert(InsertRequest request, ServerCallContext context)
        {
            try
            {
                var rows = request.Rows
                    .Select(x => new List<object>(x.Items
                    .Select(i =>
                    {
                        var res = new object();
                        try
                        {
                            res = JsonSerializer.Deserialize<object>(i);
                        }
                        catch
                        {
                            res = JsonSerializer.Deserialize<object>($"\"{i}\"");
                        }

                        return res;
                    }))).ToList();

                await _tableDal.InsertData(request.TableName, request.DbName, rows);


                var response = new BaseReply()
                {
                    Code = 200
                };

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Rows inserted: " + request.TableName + ", " + request.Rows.Count);
                Console.WriteLine("***************************************************************************************************************");

                return response;
            }
            catch (Exception ex)
            {
                return new BaseReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace };
            }
        }

        public override async Task<SelectReply> Select(SelectRequest request, ServerCallContext context)
        {
            try
            {
                var requestSelect = new RequestHelper.SelectRequest
                {
                    Offset = request.Offset,
                    Top = request.Top,
                    Conditions = request.Conditions
                    .ToDictionary(x => x.Key, x => _grpcModelMapper.GetValidatorsDtoFromGrpcValidators(x.Value))
                };
                var data = await _tableDal.Select(request.TableName, request.DbName, requestSelect);

                var response = new SelectReply()
                {
                    Code = 200
                };
                Parallel.ForEach(data.data, (row) => {
                    var resultRow = new Row();
                    resultRow.Items.AddRange(row.Select(x => x.ToString()));
                    response.Rows.Add(resultRow);
                });

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Rows selected: " + request.TableName);
                Console.WriteLine("***************************************************************************************************************");

                return response;
            }
            catch (Exception ex)
            {
                return new SelectReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace };
            }
        }

        public override async Task<BaseReply> Update(UpdateRequest request, ServerCallContext context)
        {
            try
            {
                var rows = request.Rows
                    .Select(x => new List<object>(x.Items
                    .Select(i =>
                    {
                        var res = new object();
                        try
                        {
                            res = JsonSerializer.Deserialize<object>(i);
                        }
                        catch
                        {
                            res = JsonSerializer.Deserialize<object>($"\"{i}\"");
                        }

                        return res;
                    }))).ToList();

                await _tableDal.UpdateData(request.TableName, request.DbName, rows);


                var response = new BaseReply()
                {
                    Code = 200
                };

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine("Rows updated: " + request.TableName + ", " + request.Rows.Count);
                Console.WriteLine("***************************************************************************************************************");

                return response;
            }
            catch (Exception ex)
            {
                return new BaseReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace };
            }
        }

        public override async Task<SelectReply> Union(UnionRequest request, ServerCallContext context)
        {
            try
            {
                var data = await _tableDal.Union(request.FirstTableName, 
                    request.DbName, new List<string> { request.SecondTableName});

                var response = new SelectReply()
                {
                    Code = 200
                };
                Parallel.ForEach(data.data, (row) => {
                    var resultRow = new Row();
                    resultRow.Items.AddRange(row.Select(x => x.ToString()));
                    response.Rows.Add(resultRow);
                });

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("***************************************************************************************************************");
                Console.WriteLine($"Union tables {request.FirstTableName} and {request.SecondTableName}");
                Console.WriteLine("***************************************************************************************************************");

                return response;
            }
            catch (Exception ex)
            {
                return new SelectReply() { Code = 400, Message = ex.Message, StackTrace = ex.StackTrace };
            }
        }
    }
}
