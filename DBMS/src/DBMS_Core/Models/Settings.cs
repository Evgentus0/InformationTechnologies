using DBMS_Core.Sources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DBMS_Core.Models
{
    public class Settings
    {
        [JsonPropertyName("rootPath")]
        public string RootPath { get; set; }
        [JsonPropertyName("fileSize")]
        public long FileSize { get; set; }
        public SupportedSources DefaultSource { get; set; }

        public override bool Equals(object obj)
        {
            var settings = (Settings)obj;

            return RootPath == settings.RootPath
                && FileSize == settings.FileSize
                && DefaultSource == settings.DefaultSource;
        }
    }
}
