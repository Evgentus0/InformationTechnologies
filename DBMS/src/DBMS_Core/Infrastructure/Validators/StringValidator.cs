using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DBMS_Core.Infrastructure.Validators
{
    public class StringValidator: IValidator
    {
        public object Value { get; set; }
        public int Operation { get; set; }
        public string Type => typeof(StringValidator).AssemblyQualifiedName;
        public string ValueType => typeof(string).AssemblyQualifiedName;

        private string _checkValue;
        private StringValidatorOperation _operation;
        private Dictionary<StringValidatorOperation, Func<string, bool>> _operationFunc;

        private List<StringValidatorOperation> _lengthOperations = new List<StringValidatorOperation>
        {
            StringValidatorOperation.LengthGreater,
            StringValidatorOperation.LengthGreaterOrEqual,
            StringValidatorOperation.LengthLess,
            StringValidatorOperation.LengthLessOrEqual
        };

        public StringValidator(StringValidatorOperation operation, string value)
        {
            _operation = operation;
            _checkValue = value;

            Value = value;
            Operation = (int)operation;

            if(!CheckIntegerValueForLenthOperation(operation, _checkValue, out int intValue))
            {
                throw new ArgumentException($"Incorrect value for string length! Value: {value}");
            }

            _operationFunc = new Dictionary<StringValidatorOperation, Func<string, bool>>
            {
                [StringValidatorOperation.LengthGreater] = new Func<string, bool>(x => x.Length > intValue),
                [StringValidatorOperation.LengthGreaterOrEqual] = new Func<string, bool>(x => x.Length >= intValue),
                [StringValidatorOperation.LengthLess] = new Func<string, bool>(x => x.Length < intValue),
                [StringValidatorOperation.LengthLessOrEqual] = new Func<string, bool>(x => x.Length <= intValue),
                [StringValidatorOperation.StartWith] = new Func<string, bool>(x => x.StartsWith(_checkValue)),
                [StringValidatorOperation.Contains] = new Func<string, bool>(x => x.Contains(_checkValue)),
                [StringValidatorOperation.EndWith] = new Func<string, bool>(x => x.EndsWith(_checkValue)),
                [StringValidatorOperation.NotEmpty] = new Func<string, bool>(x => !string.IsNullOrWhiteSpace(x))
            };
        }

        private bool CheckIntegerValueForLenthOperation(StringValidatorOperation operation, string value, out int result)
        {
            if (_lengthOperations.Contains(operation))
            {
                return int.TryParse(value, out result);
            }
            result = default;
            return true;
        }

        public void InitializeWithProperty()
        {
            _operation = (StringValidatorOperation)Operation;
            _checkValue = (string)Value;

            if (!CheckIntegerValueForLenthOperation(_operation, _checkValue, out int intValue))
            {
                throw new ArgumentException($"Incorrect value for string length! Value: {_checkValue}");
            }

            _operationFunc = new Dictionary<StringValidatorOperation, Func<string, bool>>
            {
                [StringValidatorOperation.LengthGreater] = new Func<string, bool>(x => x.Length > intValue),
                [StringValidatorOperation.LengthGreaterOrEqual] = new Func<string, bool>(x => x.Length >= intValue),
                [StringValidatorOperation.LengthLess] = new Func<string, bool>(x => x.Length < intValue),
                [StringValidatorOperation.LengthLessOrEqual] = new Func<string, bool>(x => x.Length <= intValue),
                [StringValidatorOperation.StartWith] = new Func<string, bool>(x => x.StartsWith(_checkValue)),
                [StringValidatorOperation.Contains] = new Func<string, bool>(x => x.Contains(_checkValue)),
                [StringValidatorOperation.EndWith] = new Func<string, bool>(x => x.EndsWith(_checkValue)),
                [StringValidatorOperation.NotEmpty] = new Func<string, bool>(x => !string.IsNullOrWhiteSpace(x))
            };
        }

        public StringValidator(StringValidatorOperation operation, int value) : this(operation, value.ToString()) { }

        public bool IsValid(object value)
        {
            if (value.GetType() == typeof(JsonElement))
            {
                value = ((JsonElement)value).GetString();
            }

            return _operationFunc[_operation]((string)value);
        }

    }

    public enum StringValidatorOperation
    {
        LengthGreater,
        LengthGreaterOrEqual,
        LengthLess,
        LengthLessOrEqual,
        StartWith,
        Contains,
        EndWith,
        NotEmpty
    }
}
