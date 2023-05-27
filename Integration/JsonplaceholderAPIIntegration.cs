using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appfunko.DTO;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace appfunko.Integration
{
    public class JsonplaceholderAPIIntegration
    {
        private readonly ILogger<CurrencyExchangeApiIntegration> _logger;
        private const string API_URL="https://jsonplaceholder.typicode.com/todos/";
        private readonly HttpClient httpClient;

        public JsonplaceholderAPIIntegration(ILogger<CurrencyExchangeApiIntegration> logger){
            _logger = logger;
            httpClient = new HttpClient();
        }

        public async Task<List<TodoDTO>> GetAll(){
            string requestUrl = $"{API_URL}";
            List<TodoDTO> listado = new List<TodoDTO>();
            try{
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    listado =  await response.Content.ReadFromJsonAsync<List<TodoDTO>>() ?? new List<TodoDTO>();
  
                }
            }catch(Exception ex){
                _logger.LogDebug($"Error al llamar a la API: {ex.Message}");
            }
            return listado;
         }
    }
}