using DataFeedAPITP2.Daily;
using DataFeedAPITP2.Forecast;

namespace DataFeedAPITP2.Pollution;

public class RootPollution
{
    public Coordinates coord { get; set; }
    public List<ListPollution> list { get; set; }
}