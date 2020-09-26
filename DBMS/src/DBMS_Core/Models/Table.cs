using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using DBMS_Core.Converters;
using DBMS_Core.Interfaces;

namespace DBMS_Core.Models
{
    public class Table
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("sources")]
        [JsonConverter(typeof(SourceListConverter))]
        public List<ISource> Sources { get; set; }
        [JsonPropertyName("schema")]
        public TableSchema Schema { get; set; }
        [JsonIgnore]
        public List<List<object>> Items { get; set; }

        public Table()
        {
            Sources = new List<ISource>();
            Items = new List<List<object>>();
            Schema = new TableSchema();
        }
    }
}