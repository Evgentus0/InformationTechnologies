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
            return typeToConvert == typeof(List<IValidator>);
        }

        public override List<IValidator> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var jsonObject = JsonDocument.ParseValue(ref reader);

            var array = JsonSerializer.Deserialize<JsonElement>(jsonObject.RootElement.GetString());
            var result = array.EnumerateArray().Select(x =>
            {
                var validatorType = Type.GetType(x.GetProperty(Constants.TypeProperty).GetString());
                var element = JsonSerializer.Deserialize(x.GetRawText(), validatorType);
                IValidator validatorElement = (IValidator)element;
                validatorElement.Operation = x.GetProperty("operation").GetInt32();

                var valueType = Type.GetType(x.GetProperty("valueType").GetString());
                validatorElement.Value = JsonSerializer.Deserialize(x.GetProperty("value").GetRawText(), valueType);
                validatorElement.InitializeWithProperty();

                return (IValidator)element;
            });

            return result.ToList();
        }

        public override void Write(Utf8JsonWriter writer, List<IValidator> value, JsonSerializerOptions options)
        {
            var dataString = JsonSerializer.Serialize(value);
            JsonEncodedText text = JsonEncodedText.Encode(dataString, JavaScriptEncoder.UnsafeRelaxedJsonEscaping);

            writer.WriteStringValue(text);
        }
    }
}
