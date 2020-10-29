using DBMS.SharedModels.DTO;
using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBMS.WebApi.Infrastructure.Interfaces
{
    public interface IDbMapper
    {
        List<IValidator> GetValidators(List<Validator> validators);
        List<SharedModels.DTO.Table> GetDtoTables(List<DBMS_Core.Models.Table> tables);
    }
}
