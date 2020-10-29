using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DBMS.SharedModels.DTO
{
    public class Source
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
