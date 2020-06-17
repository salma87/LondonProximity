using System.Text.Json.Serialization;

namespace LondonProximity.API
{
    public class User
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
        [JsonPropertyName("email")]
        public string EmailAddress { get; set; }
        [JsonPropertyName("ip_address")]
        public string IPAddress { get; set; }
        [JsonPropertyName("latitude")]
        public object Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public object Longitude { get; set; }
    }
}
