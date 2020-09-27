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

        private T _checkValue;
        private NumericValidatorOperation _operation;
        private Dictionary<NumericValidatorOperation, Func<T, bool>> _operationFunc;

        public void InitializeWithProperty()
        {
            _operation = (NumericValidatorOperation)Operation;
            _checkValue = (T)Value;

            _operationFunc = new Dictionary<NumericValidatorOperation, Func<T, bool>>
            {
                [NumericValidatorOperation.Greater] = new Func<T, bool>(x => x.CompareTo(_checkValue) == 1),
                [NumericValidatorOperation.GreaterOrEqual] = new Func<T, bool>(x => x.CompareTo(_checkValue) == 1 || x.CompareTo(_checkValue) == 0),
                [NumericValidatorOperation.Less] = new Func<T, bool>(x => x.CompareTo(_checkValue) == -1),
                [NumericValidatorOperation.LessOrEqual] = new Func<T, bool>(x => x.CompareTo(_checkValue) == -1 || x.CompareTo(_checkValue) == 0),
            };
        }

        public NumericValidator() { }

        public NumericValidator(NumericValidatorOperation operation, T value)
        {
            _operation = operation;
            _checkValue = value;

            Value = value;
            Operation = (int)operation;

            _operationFunc = new Dictionary<NumericValidatorOperation, Func<T, bool>>
            {
                [NumericValidatorOperation.Greater] = new Func<T, bool>(x => x.CompareTo(_checkValue) == 1),
                [NumericValidatorOperation.GreaterOrEqual] = new Func<T, bool>(x => x.CompareTo(_checkValue) == 1 || x.CompareTo(_checkValue) == 0),
                [NumericValidatorOperation.Less] = new Func<T, bool>(x => x.CompareTo(_checkValue) == -1),
                [NumericValidatorOperation.LessOrEqual] = new Func<T, bool>(x => x.CompareTo(_checkValue) == -1 || x.CompareTo(_checkValue) == 0),
            };
        }
        public bool IsValid(object actualValue)
        {
            if(actualValue.GetType() == typeof(JsonElement))
            {
                actualValue = ((JsonElement)actualValue).GetDouble();
            }

            return _operationFunc[_operation]((T)actualValue);
        }
    }

    public enum NumericValidatorOperation
    {
        Greater,
        GreaterOrEqual,
        Less,
        LessOrEqual
    }
}
