using DBMS_Core.Interfaces;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Factories.Interfaces
{
    public interface IValidatorsFactory
    {
        IValidator GetValidator(SupportedTypes valueType, int operation, object value);
    }
}
