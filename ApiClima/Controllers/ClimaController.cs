using ApiClima.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiClima.Controllers
{
    [ApiController]
    public class ClimaController : ControllerBase
    {
        private readonly IHttpClientFactory? _httpClientFactory;

        public ClimaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("sao-paulo")]
        public async Task<IActionResult> ObterFrase()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                "https://api.open-meteo.com/v1/forecast?latitude=-23.55&longitude=-46.64&current_weather=true");

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode == false)
            {
                return StatusCode(StatusCodes.Status502BadGateway);
            }

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var infoClimaApi = await JsonSerializer.DeserializeAsync<InfoClima>(contentStream);

            if (infoClimaApi == null)
            {
                return NotFound();
            }

            var fraseRetorno = new FraseRetorno("São Paulo", infoClimaApi.CurrentWeather.Temperature, "celsius");

            var fraseFinal = fraseRetorno.ObterFraseFinal();

            return Ok(fraseFinal);            
        }

        [HttpGet("sao-paulo-refatorado")]
        public async Task<IActionResult> ObterFraseRefatorado()
        {
            var infoClimaApi = await ObterClimaApi();

            var fraseFinal = ObterFraseFinal("São Paulo", infoClimaApi);

            return Ok(fraseFinal);
        }

        #region Métodos Privados

        private async Task<InfoClima?> ObterClimaApi()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                "https://api.open-meteo.com/v1/forecast?latitude=-23.55&longitude=-46.64&current_weather=true"
                );

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var infoClimaApi = await JsonSerializer.DeserializeAsync<InfoClima>(contentStream);

            return infoClimaApi;
        }

        private string ObterFraseFinal(string cidade, InfoClima infoClima)
        {
            var fraseRetorno = new FraseRetorno(cidade, infoClima.CurrentWeather.Temperature, "celsius");

            return fraseRetorno.ObterFraseFinal();
        }

        #endregion

        [HttpGet("valor")]
        public async Task<IActionResult> ObterValor()
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                "https://api.open-meteo.com/v1/forecast?latitude=-23.55&longitude=-46.64&current_weather=true"
                );

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var infoClimaApi = await JsonSerializer.DeserializeAsync<InfoClima>(contentStream);

            return Ok(infoClimaApi.CurrentWeather.Temperature);
        }
    }
}