using DataFeedAPITP2.Daily;

namespace DataFeedAPITP2.Forecast;

public class List
{
    public int dt { get; set; }
    public MainForecast main { get; set; }
    public List<WeatherForecast> weather { get; set; }
    public Clouds clouds { get; set; }
    public WindForecast wind { get; set; }
    public int visibility { get; set; }
    public double pop { get; set; }
    public SysForecast sys { get; set; }
    public string dt_txt { get; set; }
}