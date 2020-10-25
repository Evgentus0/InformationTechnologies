using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DBMS.SharedModels.DTO
{
    public class Validator
    {
        [JsonPropertyName("value")]
        public object Value { get; set; }
        [JsonPropertyName("operation")]
        public int Operation { get; set; }
        [JsonPropertyName("valueType")]
        public string ValueType { get; set; }
    }
}
