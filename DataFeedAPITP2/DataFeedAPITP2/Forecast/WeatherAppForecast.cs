using System.Linq.Expressions;
using DataFeedAPITP2.Daily;
using DataFeedAPITP2.Forecast;
using Newtonsoft.Json; 

namespace DataFeedAPITP2;

public class WeatherAppForecast
{
    private HttpClient HttpClient { get; set; }
    private const string API_KEY = "d139c09a3de75bea703007eca5f55d60";
    
    public WeatherAppForecast()
    {
        HttpClient = new HttpClient();
    }
    
    private Uri BuildUriForecast(double lat, double lon)
    {
        return new Uri(
            $"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&appid={API_KEY}");
    }
    public async Task GetWeatherAtCoord(Coordinates coord)
    {
        var uri = BuildUriForecast(coord.lat, coord.lon);
        RootForecast deserializeObject = await ConnectForecast(uri);
        Console.WriteLine(deserializeObject.city.country);
        
    }
    public async Task GetWeatherForecast(Coordinates coord)
    {
        var uri = BuildUriForecast(coord.lat, coord.lon);
        RootForecast deserializeObject = await ConnectForecast(uri);

        foreach (List element in deserializeObject.list)
        {
            Console.WriteLine("Date time : {0}",element.dt_txt);
            Console.WriteLine("Weather : {0}",element.weather[0].description);
            Console.WriteLine("Temperature °C: {0}",(Math.Round(Convert.ToDouble(element.main.temp - 273.15))));
            Console.WriteLine("Temperature Max °C: {0}",(Math.Round(Convert.ToDouble(element.main.temp_max - 273.15))));
            Console.WriteLine("Temperature Min °C: {0}",(Math.Round(Convert.ToDouble(element.main.temp_min - 273.15))));
            Console.WriteLine("Temperature feels like °C: {0}",(Math.Round(Convert.ToDouble(element.main.feels_like - 273.15))));
            Console.WriteLine("Humidity % : {0}",element.main.humidity);
            Console.WriteLine("Pressure hPA: {0}",element.main.pressure);
            Console.WriteLine("Visibility meters: {0}",element.visibility);
            Console.WriteLine("Wind speed m/s: {0}",element.wind.speed);
            Console.WriteLine();

        }
        
    }


    private async Task<RootForecast> ConnectForecast(Uri uri)
    {
        RootForecast deserializeObject = new RootForecast(); 
        try
        {
            String responseBody = await HttpClient.GetStringAsync(uri);
            deserializeObject = JsonConvert.DeserializeObject<RootForecast>(responseBody);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException caught ! ");
            Console.WriteLine("Message :{0} ",e.Message);
        }

        return deserializeObject;
    }
    
}