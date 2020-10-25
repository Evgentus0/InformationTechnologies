using DBMS.SharedModels.DTO;
using DBMS.WebApi.Infrastructure.Interfaces;
using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Interfaces;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DBMS.WebApi.Infrastructure.Helpers
{
    public class DbMapper : IDbMapper
    {
        public List<SharedModels.DTO.Table> GetDtoTables(List<DBMS_Core.Models.Table> tables)
        {

            return tables.Select(x => new Table
            {
                Name = x.Name,
                Sources = x.Sources.Select(x => new Source
                {
                    Type = x.Type,
                    Url = x.Url
                }).ToList(),
                TableSchema = new TableSchema
                {
                    Fields = x.Schema.Fields.Select(x => new Field
                    {
                        Name = x.Name,
                        Type = x.Type,
                        Validators = x.Validators.Select(x => new Validator
                        {
                            Operation = x.Operation,
                            Value = x.Value,
                            ValueType = x.ValueType
                        }).ToList()
                    }).ToList()
                }
            }).ToList();
        }

        public List<IValidator> GetValidators(List<Validator> validators)
        {
            return validators.Select(x => ValidatorsFactory.GetValidator(_typesDic[x.ValueType], x.Operation, 
                JsonSerializer.Deserialize(((JsonElement)x.Value).GetRawText(), Type.GetType(x.ValueType)))).ToList();
        }

        private Dictionary<string, SupportedTypes> _typesDic =>
            new Dictionary<string, SupportedTypes>
            {
                [typeof(int).AssemblyQualifiedName] = SupportedTypes.Integer,
                [typeof(double).AssemblyQualifiedName] = SupportedTypes.Real,
                [typeof(char).AssemblyQualifiedName] = SupportedTypes.Char,
                [typeof(string).AssemblyQualifiedName] = SupportedTypes.String,
                [typeof(RealInterval).AssemblyQualifiedName] = SupportedTypes.RealInterval,
                [typeof(Picture).AssemblyQualifiedName] = SupportedTypes.Picture,
            };
    }
}
