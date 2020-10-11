using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DBMS_Core.Infrastructure.Validators
{
    public class NumericValidator<T> : IValidator where T :  IComparable<T>
    {
        public object Value { get; set; }
        public int Operation { get; set; }
        public string Type => typeof(NumericValidator<T>).AssemblyQualifiedName;
        public string ValueType => typeof(T).AssemblyQualifiedName;
        public string OperationType => typeof(NumericValidatorOperation).AssemblyQualifiedName;

        private T _checkValue;
        private NumericValidatorOperation _operation;
        private Dictionary<NumericValidatorOperation, Func<T, bool>> _operationFunc;

        public void InitializeWithProperty()
        {
            _operation = (NumericValidatorOperation)Operation;
            _checkValue = (T)Value;

            _operationFunc = GetOperationList(_checkValue);
        }

        public NumericValidator() { }

        public NumericValidator(NumericValidatorOperation operation, T value)
        {
            _operation = operation;
            _checkValue = value;

            Value = value;
            Operation = (int)operation;

            _operationFunc = GetOperationList(_checkValue);
        }

        private Dictionary<NumericValidatorOperation, Func<T, bool>> GetOperationList(T checkValue)
        {
            return new Dictionary<NumericValidatorOperation, Func<T, bool>>
            {
                [NumericValidatorOperation.Equal] = new Func<T, bool>(x => x.CompareTo(checkValue) == 0),
                [NumericValidatorOperation.NotEqual] = new Func<T, bool>(x => x.CompareTo(checkValue) != 0),
                [NumericValidatorOperation.Greater] = new Func<T, bool>(x => x.CompareTo(checkValue) == 1),
                [NumericValidatorOperation.GreaterOrEqual] = new Func<T, bool>(x => x.CompareTo(_checkValue) == 1 || x.CompareTo(checkValue) == 0),
                [NumericValidatorOperation.Less] = new Func<T, bool>(x => x.CompareTo(checkValue) == -1),
                [NumericValidatorOperation.LessOrEqual] = new Func<T, bool>(x => x.CompareTo(checkValue) == -1 || x.CompareTo(checkValue) == 0),
            };
        }

        public bool IsValid(object actualValue)
        {
            if(actualValue.GetType() == typeof(JsonElement))
            {
                if (System.Type.GetType(ValueType) == typeof(double))
                {
                    actualValue = ((JsonElement)actualValue).GetDouble();
                }
                else if(System.Type.GetType(ValueType) == typeof(int))
                {
                    actualValue = ((JsonElement)actualValue).GetInt32();
                }
                else
                {
                    throw new ArgumentException("Not supported type: " + ValueType);
                }
            }

            return _operationFunc[_operation]((T)actualValue);
        }
    }

    public enum NumericValidatorOperation
    {
        Equal,
        NotEqual,
        Greater,
        GreaterOrEqual,
        Less,
        LessOrEqual
    }
}
