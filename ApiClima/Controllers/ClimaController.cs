using ApiClima.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiClima.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClimaController : ControllerBase
    {
        private readonly IHttpClientFactory? _httpClientFactory;

        public ClimaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("sao-paulo")]
        public async Task<IActionResult> Get()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                "https://api.open-meteo.com/v1/forecast?latitude=-23.55&longitude=-46.64&current_weather=true");

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            InfoClima? infoClima = null;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                var infoClimaApi = await JsonSerializer.DeserializeAsync<InfoClima>(contentStream);

                if (infoClimaApi != null)
                {
                    infoClima = infoClimaApi;
                }
            }

            if (infoClima == null)
            {
                return NotFound();
            }

            return Ok($"Em São Paulo faz {infoClima.CurrentWeather.Temperature}º graus celsius!");
        }
    }
}