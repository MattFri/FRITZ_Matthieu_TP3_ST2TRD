﻿namespace DataFeedAPITP2.Forecast;

public class RootForecast
{
    public string cod { get; set; }
    public int message { get; set; }
    public int cnt { get; set; }
    public List<List> list { get; set; }
    public City city { get; set; }
}