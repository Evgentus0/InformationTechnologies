using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DBMS.SharedModels.DTO
{
    public class Table
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("tableSchema")]
        public TableSchema TableSchema { get; set; }
        [JsonPropertyName("sources")]
        public List<Source> Sources { get; set; }

        public Table()
        {
            TableSchema = new TableSchema();
            Sources = new List<Source>();
        }
    }
}
