using System.Text.Json.Serialization;

namespace ApiClima.Models
{
    public class InfoClima
    {
        [JsonPropertyName("current_weather")]
        public CurrentWeather CurrentWeather { get; set; }

        [JsonPropertyName("latitude")]
        public decimal Latitude { get; set; }
    }
}