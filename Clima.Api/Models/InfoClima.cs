using System.Text.Json.Serialization;

namespace Clima.Api.Models;

public class InfoClima
{
    [JsonPropertyName("current_weather")]
    public CurrentWeather CurrentWeather { get; set; }
}