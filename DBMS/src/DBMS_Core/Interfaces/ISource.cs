using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using DBMS_Core.Converters;

namespace DBMS_Core.Interfaces
{
    public interface ISource
    {
        [JsonPropertyName("url")]
        string Url { get; set; }
        [JsonPropertyName("type")]
        string Type { get; }
        [JsonIgnore]
        long SizeInBytes { get; }

        List<List<object>> GetData();
        Task<List<List<object>>> GetDataAsync();

        void WriteData(List<List<object>> data);
        Task WriteDataAsync(List<List<object>> data);
    }
}
