using System.Text.Json.Serialization;

namespace Clima.Api.Models;

public class CurrentWeather
{
    [JsonPropertyName("temperature")]
    public decimal Temperature { get; set; }
}