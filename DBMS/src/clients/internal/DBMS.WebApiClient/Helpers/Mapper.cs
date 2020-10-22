using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Interfaces;
using DBMS_Core.Models;
using DBMS_Core.Models.Types;
using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dto = DBMS.SharedModels.DTO;

namespace DBMS.WebApiClient.Helpers
{
    class Mapper
    {
        internal DBMS_Core.Models.Table FromTableDtoToTable(SharedModels.DTO.Table table)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<DBMS_Core.Models.Table> FromTableDtoToTable(List<SharedModels.DTO.Table> tables)
        {
            return tables.Select(t => new DBMS_Core.Models.Table
            {
                Name = t.Name,
                Schema = new DBMS_Core.Models.TableSchema
                {
                    Fields = t.TableSchema.Fields.Select(f => new Field
                    {
                        Name = f.Name,
                        Type = f.Type,
                        Validators = f.Validators.Select(x => ValidatorsFactory.GetValidator(x.ValueType, x.Operation, x.Value)).ToList()
                    }).ToList()
                },
                Sources = t.Sources.Select(x => SourceFactory.GetSourceObject((SupportedSources)Enum.Parse(typeof(SupportedSources), x.Type), x.Url, t.Name)).ToList()
            });
        }

        internal List<Dto.Validator> GetDtoValidators(List<IValidator> validators)
        {
            return validators.Select(x => new Dto.Validator
            {
                Operation = x.Operation,
                Value = x.Value,
                ValueType = (SupportedTypes)Enum.Parse(typeof(SupportedTypes), x.ValueType)
            }).ToList();
        }
    }
}
