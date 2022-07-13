using DataFeedAPITP2.Daily;
using DataFeedAPITP2.Forecast;
using Newtonsoft.Json;

namespace DataFeedAPITP2.Pollution;

public class PollutionApp
{
    private HttpClient HttpClient { get; set; }
    private const string API_KEY = "d139c09a3de75bea703007eca5f55d60";
    
    public PollutionApp()
    {
        HttpClient = new HttpClient();
    }
    
    private Uri BuildUriPollution(double lat, double lon)
    {
        return new Uri(
            $"http://api.openweathermap.org/data/2.5/air_pollution?lat={lat}&lon={lon}&appid={API_KEY}");
    }

    private async Task<RootPollution> ConnectPollution(Uri uri)
    {
        RootPollution DeserializeObject = new RootPollution(); 
        try
        {
            String ResponseBody = await HttpClient.GetStringAsync(uri);
            DeserializeObject = JsonConvert.DeserializeObject<RootPollution>(ResponseBody);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException caught ! ");
            Console.WriteLine("Message :{0} ",e.Message);
        }
        return DeserializeObject;
    }

    public async Task GetPollution(Coordinates coord)
    {
        var uri = BuildUriPollution(coord.lat, coord.lon);
        RootPollution deserializeObject = await ConnectPollution(uri);
        
        Console.WriteLine("The air quality 1/5 = Very Good, 2/5 = Fair, 3/5 = Moderate, 4/5 = Poor, 5/5 = Very Poor");
        Console.WriteLine("Air quality : {0}",deserializeObject.list[0].main.aqi);
        Console.WriteLine("CO : {0}μg/m3",deserializeObject.list[0].components.co);
        Console.WriteLine("NO : {0}μg/m3",deserializeObject.list[0].components.no);
        Console.WriteLine("NO2 : {0}μg/m3",deserializeObject.list[0].components.no2);
        Console.WriteLine("O3 : {0}μg/m3",deserializeObject.list[0].components.o3);
        Console.WriteLine("SO2 : {0}μg/m3",deserializeObject.list[0].components.so2);
        Console.WriteLine("PM2_5 : {0}μg/m3",deserializeObject.list[0].components.pm2_5);
        Console.WriteLine("PM10 : {0}μg/m3",deserializeObject.list[0].components.pm10);
        Console.WriteLine("NH3 : {0}μg/m3",deserializeObject.list[0].components.nh3);
    }
}