using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using DBMS_Core.Models.Types;
using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Dto = DBMS.SharedModels.DTO;

namespace DBMS.WebApiClient.Helpers
{
    class Mapper
    {
        internal IEnumerable<DBMS_Core.Models.Table> FromTableDtoToTable(List<SharedModels.DTO.Table> tables)
        {
            return tables.Select(t => new DBMS_Core.Models.Table
            {
                Name = t.Name,
                Schema = new DBMS_Core.Models.TableSchema
                {
                    Fields = t.TableSchema?.Fields?.Select(f => new Field
                    {
                        Name = f.Name,
                        Type = f.Type,
                        Validators = f.Validators?.Select(x => ValidatorsFactory
                        .GetValidator(_typesDic[x.ValueType], x.Operation,
                        JsonSerializer.Deserialize(((JsonElement)x.Value).GetRawText(), Type.GetType(x.ValueType)))).ToList()
                    }).ToList()
                },
                //Sources = t.Sources?.Select(x => SourceFactory.GetSourceObject((SupportedSources)Enum.Parse(typeof(SupportedSources), x.Type), x.Url, t.Name)).ToList()
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

        internal List<Dto.Validator> GetDtoValidators(List<IValidator> validators)
        {
            return validators.Select(x => new Dto.Validator
            {
                Operation = x.Operation,
                Value = x.Value,
                ValueType = x.ValueType,
            }).ToList();
        }
    }
}
