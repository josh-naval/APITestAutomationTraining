using FinalProject.RestfulBookerHerokuApp.CustomConverters;
using Newtonsoft.Json;

namespace FinalProject.RestfulBookerHerokuApp.Models
{
    [JsonConverter(typeof(UserConverter))]
    public class User
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
