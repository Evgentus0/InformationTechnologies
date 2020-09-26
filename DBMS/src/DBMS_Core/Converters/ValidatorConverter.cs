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
    public class ValidatorConverter: JsonConverter<List<IValidator>>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return base.CanConvert(typeToConvert);
        }

        public override List<IValidator> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObject = JsonDocument.ParseValue(ref reader);

            var result = jsonObject.RootElement.EnumerateArray().Select(x =>
            {
                var sourceType = Type.GetType(x.GetProperty(Constants.TypeProperty).GetString());
                var element = JsonSerializer.Deserialize(x.GetRawText(), sourceType);

                return (IValidator)element;
            });

            return (List<IValidator>)result;
        }

        public override void Write(Utf8JsonWriter writer, List<IValidator> value, JsonSerializerOptions options)
        {
            var dataString = JsonSerializer.Serialize(value);
            JsonEncodedText text = JsonEncodedText.Encode(dataString, JavaScriptEncoder.UnsafeRelaxedJsonEscaping);

            writer.WriteStringValue(text);
        }
    }
}
