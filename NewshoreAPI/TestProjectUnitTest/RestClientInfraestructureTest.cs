using Common.Consume;

namespace TestProjectUnitTest
{
    [TestClass]
    public class RestClientInfraestructureTest
    {
        [TestMethod]
        public async Task GetTestRest()
        {
            RestClientInfraestructure restClientInfraestructure = new RestClientInfraestructure("https://recruiting-api.newshore.es/");
            var result = await restClientInfraestructure.GetAsync("api/flights/0");

            Assert.IsNotNull(result);
        }
    }
}