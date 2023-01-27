using System.Text.Json.Serialization;

namespace ApiClima.Models
{
    public class InfoClima
    {
        [JsonPropertyName("current_weather")]
        public CurrentWeather CurrentWeather { get; set; }
    }
}