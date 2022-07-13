using System.Linq.Expressions;
using System.Xml.Schema;
using DataFeedAPITP2.Daily;
using DataFeedAPITP2.Pollution;

namespace DataFeedAPITP2;

public class Program
{
    public static async Task Main(String[] args)
    {
        Console.WriteLine("-------------WELCOME ON WEATHER APP-------------------");
        Console.WriteLine();
        var WeatherApp = new WeatherApp();
        var WeatherAppForecast = new WeatherAppForecast();
        var PollutionApp = new PollutionApp(); 
        
        Coordinates IstanbulCoord = new Coordinates(41.00527,28.97696);
        Coordinates Oslocoord = new Coordinates(10.752245, 59.913869);
        Coordinates Jakartacoord = new Coordinates(106.845599, -6.208763);
        Coordinates NYcoord = new Coordinates(-74.005941,40.712784);
        Coordinates Tokyocoord = new Coordinates(139.69235,35.6894);
        Coordinates Pariscoord = new Coordinates(2.352222, 48.856614);
        Coordinates Kievcoord = new Coordinates(30.523400, 50.450100);
        Coordinates Moscowcoord = new Coordinates(37.617300, 55.755826);
        Coordinates Berlincoord = new Coordinates(13.404954, 52.520007);
        Coordinates Venise = new Coordinates(12.1957, 45.2613);

        Dictionary<int, Coordinates> dicoCoord = new Dictionary<int, Coordinates>();
        
        dicoCoord.Add(1,IstanbulCoord);
        dicoCoord.Add(2,Oslocoord);
        dicoCoord.Add(3,Jakartacoord);
        dicoCoord.Add(4,NYcoord);
        dicoCoord.Add(5,Tokyocoord);
        dicoCoord.Add(6,Pariscoord);
        dicoCoord.Add(7,Kievcoord);
        dicoCoord.Add(8,Moscowcoord);
        dicoCoord.Add(9,Berlincoord);
        dicoCoord.Add(10,Venise);

        Dictionary<int, string> dicoName = new Dictionary<int, string>();
        
        dicoName.Add(1,"Istanbul");
        dicoName.Add(2,"Oslo");
        dicoName.Add(3,"Jakarta");
        dicoName.Add(4,"NY");
        dicoName.Add(5,"Tokyo");
        dicoName.Add(6,"Paris");
        dicoName.Add(7,"Kiev");
        dicoName.Add(8,"Moscow");
        dicoName.Add(9,"Berlin");
        dicoName.Add(10,"Venise");

        int choice = 0;

        do
        {
            foreach (KeyValuePair<int, string> element in dicoName)
            {
                Console.WriteLine("Choice : {0}, City : {1}.", element.Key, element.Value);
            }
            Console.WriteLine("Enter your choice.");
            choice = Convert.ToInt32(Console.ReadLine());
        } while (choice == 0);
        
        Coordinates currentcoord = dicoCoord[choice];
        String currentcity = dicoName[choice];
        Console.WriteLine();
        Console.WriteLine("----------------WEATHER----------");
        Console.WriteLine();
        Console.WriteLine("              City : "+currentcity);
        await WeatherApp.GetWeatherAtCoord(currentcoord);
        Console.WriteLine("Humidity %: {0}              Pressure hPa: {1}",await WeatherApp.GetHumidity(currentcoord),await WeatherApp.GetPressure(currentcoord));
        await WeatherApp.GetTemp(currentcoord);
        await WeatherApp.PrintSun(currentcoord);
        Console.WriteLine("Sea level: {0}             Wind speed m/s : {1}",await WeatherApp.GetSea_Level(currentcoord),await WeatherApp.GetWind(currentcoord));
        Console.WriteLine();
        Console.WriteLine("-------------AIR QUALITY---------------");
        await PollutionApp.GetPollution(currentcoord);
        Console.WriteLine();
        Console.WriteLine("-----FORECASTS OVER 5 DAYS-----");
        Console.WriteLine();
        
        await WeatherAppForecast.GetWeatherForecast(currentcoord);
        
    }
}