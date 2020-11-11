using Dto = DBMS.SharedModels.DTO;
using DBMS.SharedModels.Infrastructure.Interfaces;
using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Interfaces;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DBMS_Core.Models;
using DBMS_Core.Infrastructure.Factories.Interfaces;

namespace DBMS.SharedModels.Infrastructure.Helpers
{
    public class DbMapper : IDbMapper
    {
        private IValidatorsFactory _validatorsFactory;
        public DbMapper(IValidatorsFactory validatorsFactory)
        {
            _validatorsFactory = validatorsFactory;
        }

        public List<Dto.Table> GetDtoTables(List<DBMS_Core.Models.Table> tables)
        {

            return tables.Select(x => new Dto.Table
            {
                Name = x.Name,
                Sources = x.Sources.Select(s => new Dto.Source
                {
                    Type = s.Type,
                    Url = s.Url
                }).ToList(),
                TableSchema = new Dto.TableSchema
                {
                    Fields = x.Schema.Fields.Select(f => new Dto.Field
                    {
                        Name = f.Name,
                        Type = f.Type,
                        Validators = f.Validators.Select(v => new Dto.Validator
                        {
                            Operation = v.Operation,
                            Value = v.Value,
                            ValueType = v.ValueType
                        }).ToList()
                    }).ToList()
                }
            }).ToList();
        }

        public List<IValidator> GetValidators(List<Dto.Validator> validators)
        {
            return validators.Select(x => _validatorsFactory.GetValidator(_typesDic[x.ValueType], x.Operation, 
                JsonSerializer.Deserialize(((JsonElement)x.Value).GetRawText(), Type.GetType(x.ValueType)))).ToList();
        }

        public List<Dto.Validator> GetDtoValidators(List<IValidator> validators)
        {
            return validators.Select(x => new Dto.Validator
            {
                Operation = x.Operation,
                Value = x.Value,
                ValueType = x.ValueType,
            }).ToList();
        }

        public IEnumerable<Table> FromTableDtoToTable(List<Dto.Table> tables)
        {
            return tables.Select(t => new Table
            {
                Name = t.Name,
                Schema = new TableSchema
                {
                    Fields = t.TableSchema?.Fields?.Select(f => new Field
                    {
                        Name = f.Name,
                        Type = f.Type,
                        Validators = f.Validators?.Select(x => _validatorsFactory
                        .GetValidator(_typesDic[x.ValueType], x.Operation,
                        JsonSerializer.Deserialize(((JsonElement)x.Value).GetRawText(), Type.GetType(x.ValueType)))).ToList()
                    }).ToList()
                },
            });
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
