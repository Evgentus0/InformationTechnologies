using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DBMS_Core.Interfaces
{
    public interface IValidator
    {
        [JsonPropertyName("value")]
        object Value { get; }
        [JsonPropertyName("operation")]
        int Operation { get; }
        [JsonPropertyName(Constants.TypeProperty)]
        string Type { get; }

        bool IsValid(object value); 
    }
}
