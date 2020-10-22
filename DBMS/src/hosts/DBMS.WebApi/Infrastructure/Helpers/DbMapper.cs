using DBMS.SharedModels.DTO;
using DBMS.WebApi.Infrastructure.Interfaces;
using DBMS_Core.Infrastructure.Factories;
using DBMS_Core.Interfaces;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;
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
                            ValueType = Enum.Parse<SupportedTypes>(x.ValueType)
                        }).ToList()
                    }).ToList()
                }
            }).ToList();
        }

        public List<IValidator> GetValidators(List<Validator> validators)
        {
            return validators.Select(x => ValidatorsFactory.GetValidator(x.ValueType, x.Operation, x.Value)).ToList();
        }
    }
}
