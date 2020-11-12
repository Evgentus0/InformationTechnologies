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

        public override bool Equals(object obj)
        {
            var schema = (TableSchema)obj;
            if(schema.Fields.Count == Fields.Count)
            {
                for(int i = 0; i < Fields.Count; i++)
                {
                    if (!Fields[i].Equals(schema.Fields[i]))
                        return false;
                }
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
