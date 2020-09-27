using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DBMS_Core.Interfaces
{
    public interface IValidator
    {
        [JsonPropertyName("value")]
        object Value { get; set; }
        [JsonPropertyName("operation")]
        int Operation { get; set; }
        [JsonPropertyName(Constants.TypeProperty)]
        string Type { get; }
        [JsonPropertyName("valueType")]
        string ValueType { get; }
        void InitializeWithProperty();

        bool IsValid(object value); 
    }
}
