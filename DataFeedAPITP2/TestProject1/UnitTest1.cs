using DataFeedAPITP2;
using DataFeedAPITP2.Daily;

namespace TestProject1;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestHumidity()
    {
        var expected_humidity_type = typeof(int);
        var obj = new WeatherApp(); 
        Coordinates IstanbulCoord = new Coordinates(41.00527,28.97696);
        var test_type = obj.GetHumidity(IstanbulCoord).GetType();
        Assert.AreEqual(test_type,expected_humidity_type);
    }
}