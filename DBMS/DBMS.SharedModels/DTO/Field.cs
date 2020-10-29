using DBMS_Core.Models.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DBMS.SharedModels.DTO
{
    public class Field
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("type")]
        public SupportedTypes Type{ get; set; }
        [JsonPropertyName("validators")]
        public List<Validator> Validators { get; set; }

        public Field()
        {
            Validators = new List<Validator>();
        }

    }
}
