using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto = DBMS.SharedModels.DTO;
using Core = DBMS_Core.Models;

namespace DBMS.SharedModels.Infrastructure.Interfaces
{
    public interface IDbMapper
    {
        List<IValidator> GetValidators(List<Dto.Validator> validators);
        List<Dto.Table> GetDtoTables(List<Core.Table> tables);
        List<Dto.Validator> GetDtoValidators(List<IValidator> validators);
        IEnumerable<Core.Table> FromTableDtoToTable(List<Dto.Table> tables);
    }
}
