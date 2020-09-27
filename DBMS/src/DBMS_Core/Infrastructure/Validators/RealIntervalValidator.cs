using DBMS_Core.Interfaces;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DBMS_Core.Infrastructure.Validators
{
    public class RealIntervalValidator: IValidator
    {
        public object Value { get; set; }
        public int Operation { get; set; }
        public string Type => typeof(RealIntervalValidator).AssemblyQualifiedName;
        public string ValueType => typeof(double).AssemblyQualifiedName;

        private double _checkValue;
        private RealIntervalValidatorOperation _operation;
        private Dictionary<RealIntervalValidatorOperation, Func<RealInterval, bool>> _operationFunc;

        public RealIntervalValidator() { }

        public RealIntervalValidator(RealIntervalValidatorOperation operation, double value)
        {
            _operation = operation;
            _checkValue = value;

            Value = value;
            Operation = (int)operation;

            _operationFunc = new Dictionary<RealIntervalValidatorOperation, Func<RealInterval, bool>>
            {
                [RealIntervalValidatorOperation.InfimumGreater] = new Func<RealInterval, bool>(x => x.From > _checkValue),
                [RealIntervalValidatorOperation.InfimumGreaterOrEqual] = new Func<RealInterval, bool>(x => x.From >= _checkValue),
                [RealIntervalValidatorOperation.InfimumLess] = new Func<RealInterval, bool>(x => x.From < _checkValue),
                [RealIntervalValidatorOperation.InfimumLessOrEqual] = new Func<RealInterval, bool>(x => x.From <= _checkValue),
                [RealIntervalValidatorOperation.SupremumGreater] = new Func<RealInterval, bool>(x => x.To > _checkValue),
                [RealIntervalValidatorOperation.SupremumGreaterOrEqual] = new Func<RealInterval, bool>(x => x.To >= _checkValue),
                [RealIntervalValidatorOperation.SupremumLess] = new Func<RealInterval, bool>(x => x.To < _checkValue),
                [RealIntervalValidatorOperation.SupremumLessOrEqual] = new Func<RealInterval, bool>(x => x.To <= _checkValue)
            };
        }

        public void InitializeWithProperty()
        {
            _operation = (RealIntervalValidatorOperation)Operation;
            _checkValue = (double)Value;

            _operationFunc = new Dictionary<RealIntervalValidatorOperation, Func<RealInterval, bool>>
            {
                [RealIntervalValidatorOperation.InfimumGreater] = new Func<RealInterval, bool>(x => x.From > _checkValue),
                [RealIntervalValidatorOperation.InfimumGreaterOrEqual] = new Func<RealInterval, bool>(x => x.From >= _checkValue),
                [RealIntervalValidatorOperation.InfimumLess] = new Func<RealInterval, bool>(x => x.From < _checkValue),
                [RealIntervalValidatorOperation.InfimumLessOrEqual] = new Func<RealInterval, bool>(x => x.From <= _checkValue),
                [RealIntervalValidatorOperation.SupremumGreater] = new Func<RealInterval, bool>(x => x.To > _checkValue),
                [RealIntervalValidatorOperation.SupremumGreaterOrEqual] = new Func<RealInterval, bool>(x => x.To >= _checkValue),
                [RealIntervalValidatorOperation.SupremumLess] = new Func<RealInterval, bool>(x => x.To < _checkValue),
                [RealIntervalValidatorOperation.SupremumLessOrEqual] = new Func<RealInterval, bool>(x => x.To <= _checkValue)
            };
        }

        public bool IsValid(object value)
        {
            if (value.GetType() == typeof(JsonElement))
            {
                value = JsonSerializer.Deserialize(((JsonElement)value).GetRawText(), typeof(RealInterval));
            }

            return _operationFunc[_operation]((RealInterval)value);
        }
    }

    public enum RealIntervalValidatorOperation
    {
        InfimumGreater,
        InfimumGreaterOrEqual,
        InfimumLess,
        InfimumLessOrEqual,
        SupremumGreater,
        SupremumGreaterOrEqual,
        SupremumLess,
        SupremumLessOrEqual
    }
}
