using Microsoft.AspNetCore.Mvc.RazorPages;
using WeatherPortal.Core;

namespace WeatherPortal.Pages
{
    public class IndexModel : PageModel
    {        
        private readonly IWeatherManager _service;
        public WeatherResults WeatherResults { get; set; } = new WeatherResults();                

        public IndexModel(IWeatherManager service)
        {
            _service = service;            
        }

        public async Task OnGetAsync()
        {
            //var weatherManager = new WeatherManager();
            //WeatherResults = await weatherManager.GetWeatherAsync();
            WeatherResults = await _service.GetWeatherAsync();
        }
    }
}