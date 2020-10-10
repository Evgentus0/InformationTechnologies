using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace DBMS_Core.Models.Types
{
    public class Picture
    {
        public string Description { get; set; }
        public string Path { get; set; }
        [JsonIgnore]
        public long Size => new FileInfo(Path).Length;
    }
}
