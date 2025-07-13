using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Model;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace PruebaTecnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;

        public LoginController(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
        }

        [HttpPost("Login")]
        public async Task<LoginResponse> Login(LoginRequest user)
        {
            var url = _apiSettings.LoginUrl;

            var json = JsonConvert.SerializeObject(user);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            var respuesta = await _httpClient.PostAsync(url, contenido);

            if (respuesta.IsSuccessStatusCode)
            {
                var contenidoJson = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<LoginResponse>(contenidoJson);
                return resultado;
            }
            else if (respuesta.StatusCode.ToString() == "BadRequest")
            {
                var contenidoJson = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<LoginResponse>(contenidoJson);
                resultado.error = 1;
                return resultado;
            }
            else
            {
                throw new Exception($"Error en la solicitud: {respuesta.StatusCode}");
            }
        }

    }
}
