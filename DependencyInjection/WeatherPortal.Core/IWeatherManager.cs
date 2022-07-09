namespace WeatherPortal.Core
{
    public interface IWeatherManager
    {
        Task<WeatherResults> GetWeatherAsync();
    }
}