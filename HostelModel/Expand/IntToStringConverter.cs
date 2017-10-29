using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HostelModel.Expand
{
    public class IntToStringConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Convert.ToInt32(existingValue);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue($"{value}".ToString());

        }
    }
}
