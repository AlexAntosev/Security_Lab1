using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Security_Lab3.Http
{
    public class Http
    {
        private readonly HttpClient _httpClient;

        public Http(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<TResponse> Get<TResponse>(string url, params (string key, object value)[] parameters)
        {
            for (var index = 0; index < parameters.Length; index++)
            {
                url += index == 0 ? "?" : "&";
                var parameter = parameters[index];
                url += $"{parameter.key}={parameter.value}";
            }

            var response = await _httpClient.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }
    }
}