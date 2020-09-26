using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DBMS_Core.Models
{
    public class TableSchema
    {
        [JsonPropertyName("fields")]
        public List<Field> Fields { get; set; }

        public TableSchema()
        {
            Fields = new List<Field>();
        }
    }
}
