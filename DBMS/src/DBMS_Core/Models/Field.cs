using DBMS_Core.Converters;
using DBMS_Core.Interfaces;
using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DBMS_Core.Models
{
    public class Field
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("type")]
        public SupportedTypes Type { get; set; }
        [JsonPropertyName("validators")]
        
        public List<IValidator> Validators { get; set; }

        public Field()
        {
            Validators = new List<IValidator>();
        }
    }
}
