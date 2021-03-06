﻿using DBMS.SqlServerSource;
using DBMS.SqlServerSource.Interfaces;
using DBMS_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DBMS_Core.Converters
{
    public class SourceListConverter : JsonConverter<List<ISource>>
    {
        IDbClientFactory _dbClientFactory;

        public SourceListConverter()
        {
            _dbClientFactory = new DbClientFactory();
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(List<ISource>);
        }

        public override List<ISource> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObject = JsonDocument.ParseValue(ref reader);

            var array = JsonSerializer.Deserialize<JsonElement>(jsonObject.RootElement.GetString());
            var result = array.EnumerateArray().Select(x =>
            {
                var sourceType = Type.GetType(x.GetProperty(Constants.TypeProperty).GetString());
                var element = JsonSerializer.Deserialize(x.GetRawText(), sourceType);
                ISource sourceElement = (ISource)element;
                sourceElement.Url = x.GetProperty("url").GetString();
                sourceElement.DbClientFactory = _dbClientFactory;

                return sourceElement;
            });

            return result.ToList();
        }

        public override void Write(Utf8JsonWriter writer, List<ISource> value, JsonSerializerOptions options)
        {
            var dataString = JsonSerializer.Serialize(value);
            JsonEncodedText text = JsonEncodedText.Encode(dataString, JavaScriptEncoder.UnsafeRelaxedJsonEscaping);

            writer.WriteStringValue(text);
        }
    }
}
