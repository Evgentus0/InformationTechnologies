using DBMS.SharedModels.DTO;
using DBMS.SharedModels.ResuestHelpers;
using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;

namespace DBMS.GrpcServer.Helpers
{
    public interface IGrpcModelMapper
    {
        GetEntityReply GetGetEntityReplyFromTableDto(Table table);
        List<SharedModels.DTO.Validator> GetValidatorsDtoFromGrpcValidators(RepeatedField<Validator> validators);
    }
    public class GrpcModelMapper : IGrpcModelMapper
    {
        public GetEntityReply GetGetEntityReplyFromTableDto(Table table)
        {
            var result = new GetEntityReply
            {
                Name = table.Name
            };
            result.Columns.AddRange(table.TableSchema.Fields.Select(x => new Column
            {
                Name = x.Name,
                DataValueType = (int)x.Type
            }));
            return result;
        }

        public List<SharedModels.DTO.Validator> GetValidatorsDtoFromGrpcValidators(RepeatedField<Validator> validators)
        {
            return validators.Select(x => new SharedModels.DTO.Validator
            {
                Operation = x.Operation,
                Value = JsonSerializer.Deserialize(x.Value, Type.GetType(x.DataValueType)),
                ValueType = x.DataValueType
            }).ToList();
        }
    }
}
