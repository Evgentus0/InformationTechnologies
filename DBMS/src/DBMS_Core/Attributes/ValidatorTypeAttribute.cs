using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS_Core.Attributes
{
    internal class ValidatorTypeAttribute: Attribute
    {
        public Type ValidatorType { get; set; }

        public ValidatorTypeAttribute(Type type)
        {
            ValidatorType = type;
        }
    }
}
