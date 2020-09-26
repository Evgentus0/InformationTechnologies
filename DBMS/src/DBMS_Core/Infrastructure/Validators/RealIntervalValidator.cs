using DBMS_Core.Interfaces;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Infrastructure.Validators
{
    public class RealIntervalValidator: IValidator
    {
        public object Value { get; }
        public int Operation { get; }
        public string Type => typeof(RealIntervalValidator).AssemblyQualifiedName;

        private double _checkValue;
        private RealIntervalValidatorOperation _operation;
        private Dictionary<RealIntervalValidatorOperation, Func<RealInterval, bool>> _operationFunc;

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

        public bool IsValid(object value)
        {
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
