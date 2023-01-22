using System.Text.Json.Serialization;

namespace ApiClima.Models
{
    public class CurrentWeather
    {
        [JsonPropertyName("temperature")]
        public decimal Temperature { get; set; }
    }
}