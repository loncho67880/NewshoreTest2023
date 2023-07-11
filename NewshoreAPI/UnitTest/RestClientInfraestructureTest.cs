using Common.Consume;

namespace UnitTest
{
    public class RestClientInfraestructureTest
    {
        [Fact]
        public void GetTest()
        {
            RestClientInfraestructure restClientInfraestructure = new RestClientInfraestructure("https://recruiting-api.newshore.es/");
            var result = restClientInfraestructure.GetAsync("api/flights/0").Result;

            Assert.NotNull(result);
        }
    }
}