using DBMS_Core.Extentions;
using DBMS_Core.Interfaces;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Factories
{
    public class ValidatorsFactory
    {
        public static IValidator GetValidator(SupportedTypes valueType, Enum operation, object value)
        {
            if (valueType.IsValidatorAvailable())
            {
                var validatorType = Type.GetType(valueType.GetValidatorType());
                var validatorObject = Activator.CreateInstance(validatorType);

                var validator = (IValidator)validatorObject;

                int enumOperation = 0;
                try
                {
                    var operationType = Type.GetType(validator.OperationType);
                    enumOperation = (int)Convert.ChangeType(operation, operationType);
                }
                catch(Exception ex)
                {
                    throw new Exception("Invalid operation type!", ex);
                }

                validator.Value = value;
                validator.Operation = enumOperation;

                validator.InitializeWithProperty();

                return validator;
            }
            else
            {
                throw new Exception("This type does not support validators");
            }
        }
    }
}
