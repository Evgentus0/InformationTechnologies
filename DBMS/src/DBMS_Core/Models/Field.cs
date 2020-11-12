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
        [JsonConverter(typeof(ValidatorConverter))]

        public List<IValidator> Validators { get; set; }

        public Field()
        {
            Validators = new List<IValidator>();
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            var field = (Field)obj;

            return Name == field.Name
                && Type == field.Type;
        }
    }
}
