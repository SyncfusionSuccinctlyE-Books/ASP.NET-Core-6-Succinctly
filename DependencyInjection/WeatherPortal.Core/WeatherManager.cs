using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace WeatherPortal.Core
{
    public class WeatherManager : IWeatherManager
    {
        private static readonly HttpClient _client = new();

        public WeatherManager()
        {
            _client.BaseAddress = new Uri("https://localhost:44349/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<WeatherResults> GetWeatherAsync()
        {
            var response = await _client.GetAsync("sa-weather");
            if (response.IsSuccessStatusCode)
            {
                string apiResp = await response.Content.ReadAsStringAsync();
                var results = JsonConvert.DeserializeObject<WeatherResults>(apiResp);
                return results ?? new WeatherResults();
            }
            return new WeatherResults();
        }
    }
}