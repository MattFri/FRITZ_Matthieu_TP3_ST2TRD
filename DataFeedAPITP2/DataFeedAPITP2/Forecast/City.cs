using DataFeedAPITP2.Daily;

namespace DataFeedAPITP2.Forecast;

public class City
{
    public int id { get; set; }
    public string name { get; set; }
    public Coordinates coord { get; set; }
    public string country { get; set; }
    public int population { get; set; }
    public int timezone { get; set; }
    public int sunrise { get; set; }
    public int sunset { get; set; }
}