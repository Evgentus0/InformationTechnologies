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

        public Table()
        {
            Sources = new List<ISource>();
            Schema = new TableSchema();
        }

        public override bool Equals(object obj)
        {
            var table = (Table)obj;

            return Name == table.Name
                && Schema.Equals(table.Schema)
                && new Func<bool>(() =>
                {
                    if(Sources.Count == table.Sources.Count)
                    {
                        for(int i = 0; i < Sources.Count; i++)
                        {
                            if (Sources[i].Equals(table.Sources[i]))
                                return false;
                        }
                        return true;
                    }
                    return false;
                }).Invoke();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}