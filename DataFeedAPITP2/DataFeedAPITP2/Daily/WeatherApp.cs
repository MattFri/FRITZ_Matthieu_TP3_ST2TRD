
using Newtonsoft.Json;

namespace DataFeedAPITP2.Daily;

public class WeatherApp
{
    private HttpClient httpClient { get; set; }
    private const string ApiKey = "d139c09a3de75bea703007eca5f55d60";
    
    public WeatherApp()
    {
        httpClient = new HttpClient();
    }

    private Uri BuildUri(double lat, double lon)
    {
        return new Uri(
            $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={ApiKey}");
    }
    
    public async Task GetWeatherAtCoord(Coordinates coord)
    {
        var Uri = BuildUri(coord.lat, coord.lon) ?? throw new ArgumentNullException("coord");
        Root DeserializeObject = await ConnectDaily(Uri);
        if (DeserializeObject == null) throw new ArgumentNullException(nameof(DeserializeObject));
        Console.WriteLine("Country : {0}                Weather : {1}.",DeserializeObject.sys.country,DeserializeObject.weather[0].description);
    }
    
    
    public async Task PrintSun(Coordinates coord)
    {
        var Uri = BuildUri(coord.lat, coord.lon) ?? throw new ArgumentNullException("coord");
        Root DeserializeObject = await ConnectDaily(Uri);
        if (DeserializeObject == null) throw new ArgumentNullException(nameof(DeserializeObject));
        DateTimeOffset sunrisetime = DateTimeOffset.FromUnixTimeSeconds(DeserializeObject.sys.sunrise);
        DateTimeOffset sunsetime = DateTimeOffset.FromUnixTimeSeconds(DeserializeObject.sys.sunset);
        Console.WriteLine("Sunrise UTC : {0}      Sunset UTC : {1}.",sunrisetime.TimeOfDay,sunsetime.TimeOfDay);
    }

    public async Task<int> GetSea_Level(Coordinates coord)
    {
        var Uri = BuildUri(coord.lat, coord.lon) ?? throw new ArgumentNullException("coord");
        Root DeserializeObject = await ConnectDaily(Uri);
        if (DeserializeObject == null) throw new ArgumentNullException(nameof(DeserializeObject));
        return DeserializeObject.main.sea_level; 
    }
    public async Task<int> GetHumidity(Coordinates coord)
    {
        var Uri = BuildUri(coord.lat, coord.lon) ?? throw new ArgumentNullException("coord");
        Root DeserializeObject = await ConnectDaily(Uri);
        if (DeserializeObject == null) throw new ArgumentNullException(nameof(DeserializeObject));
        return DeserializeObject.main.humidity; 
    }

    public async Task<int> GetPressure(Coordinates coord)
    {
        var Uri = BuildUri(coord.lat, coord.lon) ?? throw new ArgumentNullException("coord");
        Root DeserializeObject = await ConnectDaily(Uri);
        return DeserializeObject.main.pressure; 
    }

    public async Task GetTemp(Coordinates coord)
    {
        var Uri = BuildUri(coord.lat, coord.lon) ?? throw new ArgumentNullException("coord");
        Root DeserializeObject = await ConnectDaily(Uri);
        if (DeserializeObject == null) throw new ArgumentNullException(nameof(DeserializeObject));
        var TempDegree = (Convert.ToDouble(DeserializeObject.main.temp) - 273.15);
        var TempMax = (Convert.ToDouble(DeserializeObject.main.temp_max) - 273.15);
        var TempMin = (Convert.ToDouble(DeserializeObject.main.temp_min) - 273.15);
        var TempFeel = (Convert.ToDouble(DeserializeObject.main.feels_like) - 273.15);
        Console.WriteLine("Temperature °C : {0}         Temperature feel like : {1}",Math.Round(TempDegree),Math.Round(TempFeel));
        Console.WriteLine("Temparature max °C : {0}     Temperature min °C : {1}",Math.Round(TempMax),Math.Round(TempMin));
    }

    public async Task<double> GetWind(Coordinates coord)
    {
        var Uri = BuildUri(coord.lat, coord.lon);
        Root DeserializeObject = await ConnectDaily(Uri);
        if (DeserializeObject == null) throw new ArgumentNullException(nameof(DeserializeObject));
        return DeserializeObject.wind.speed; 
    }
    
    private async Task<Root> ConnectDaily(Uri uri)
    {
        Root DeserializeObject = new Root();
        if (DeserializeObject == null) throw new ArgumentNullException(nameof(DeserializeObject));
        try
        {
            String ResponseBody = await httpClient.GetStringAsync(uri);
            if (ResponseBody == null) throw new ArgumentNullException(nameof(ResponseBody));
            DeserializeObject = JsonConvert.DeserializeObject<Root>(ResponseBody) ?? throw new InvalidOperationException();
        }
        catch (HttpRequestException E)
        {
            Console.WriteLine("\nException caught ! ");
            Console.WriteLine("Message :{0} ",E.Message);
        }
        return DeserializeObject;
    }
    
}