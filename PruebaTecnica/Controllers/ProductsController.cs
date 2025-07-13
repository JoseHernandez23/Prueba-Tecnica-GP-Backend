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
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ProductsModel> GetAllProducts()
        {
            var url = "https://dummyjson.com/products";

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

        [HttpGet("GetProduct/{id}")]
        public async Task<Product> GetProduct(int id)
        {
            var url = $"https://dummyjson.com/products/{id}";
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

        [HttpGet("GetAllCategories")]
        public async Task<List<string>> GetAllCategories()
        {
            var url = "https://dummyjson.com/products/category-list";
            var respuesta = await _httpClient.GetAsync(url);

            if (respuesta.IsSuccessStatusCode)
            {
                var contenidoJson = await respuesta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<string>>(contenidoJson);
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
            var url = $"https://dummyjson.com/products/category/{category}";
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
