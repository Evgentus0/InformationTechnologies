using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DBMS_Core.Models
{
    public class DataBase
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("settings")]
        public Settings Settings { get; set; }
        [JsonPropertyName("tables")]
        public List<Table> Tables { get; set; }

        public DataBase()
        {
            Tables = new List<Table>();
            Settings = new Settings();
        }
    }
}
