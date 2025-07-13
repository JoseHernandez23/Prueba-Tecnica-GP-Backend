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
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _apiSettings;

        public ProductsController(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _apiSettings = apiSettings.Value;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ProductsModel> GetAllProducts()
        {          
            var respuesta = await _httpClient.GetAsync(_apiSettings.ProductosUrlGetAll);

            if (respuesta.IsSuccessStatusCode)
            {
                var contenidoJson = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ProductsModel>(contenidoJson);
                return resultado;
            }           
            else
            {
                throw new Exception($"Error en la solicitud: {respuesta.StatusCode}");
            }
        }

        [HttpGet("GetProduct/{id}")]
        public async Task<Product> GetProduct(int id)
        {
            var url = _apiSettings.ProductosUrlGetProduct.Replace("{id}", id.ToString());
            var respuesta = await _httpClient.GetAsync(url);

            if (respuesta.IsSuccessStatusCode)
            {
                var contenidoJson = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Product>(contenidoJson);
                return resultado;
            }
            else
            {
                throw new Exception($"Error en la solicitud: {respuesta.StatusCode}");
            }
        }       

        [HttpGet("GetByCategory/{category}")]
        public async Task<ProductsModel> GetByCategory(string category)
        {
            var url = _apiSettings.ProductosUrlGetByCategory.Replace("{category}", category);
            var respuesta = await _httpClient.GetAsync(url);

            if (respuesta.IsSuccessStatusCode)
            {
                var contenidoJson = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ProductsModel>(contenidoJson);
                return resultado;
            }
            else
            {
                throw new Exception($"Error en la solicitud: {respuesta.StatusCode}");
            }
        }


    }
}
