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

        public override bool Equals(object obj)
        {
            var anotherPic = (Picture)obj;

            return Description == anotherPic.Description
                && Path == anotherPic.Path;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
