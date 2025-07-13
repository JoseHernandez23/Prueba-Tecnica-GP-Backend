using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Model;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;

namespace PruebaTecnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;

        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost("Login")]
        public async Task<LoginResponse> Login(LoginRequest user)
        {
            var url = "https://dummyjson.com/user/login";

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
