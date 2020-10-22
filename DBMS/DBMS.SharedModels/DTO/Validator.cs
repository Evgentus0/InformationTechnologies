using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMS.SharedModels.DTO
{
    public class Validator
    {
        public object Value { get; set; }
        public int Operation { get; set; }
        public SupportedTypes ValueType { get; set; }
    }
}
