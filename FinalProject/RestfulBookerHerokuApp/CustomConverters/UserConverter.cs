using FinalProject.RestfulBookerHerokuApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.RestfulBookerHerokuApp.CustomConverters
{
    public class UserConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var user = value as User;
            writer.WriteStartObject();
            writer.WritePropertyName("username");
            writer.WriteValue(user.Username);
            writer.WritePropertyName("password");
            writer.WriteValue(user.Password);
            writer.WriteEndObject();
        }
    }
}
