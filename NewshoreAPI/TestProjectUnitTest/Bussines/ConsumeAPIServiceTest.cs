using Bussines.Implementacion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnitTest;

namespace TestProjectUnitTest.Bussines
{
    [TestClass]
    public class ConsumeAPIServiceTest : BaseTest
    {
        ConsumeAPIService _consumeAPIService;

        [TestInitialize]
        public void InitSql()
        {
            //Arrange
            var inMemorySettings = new Dictionary<string, string> {
                {"RestFlights", "https://recruiting-api.newshore.es/"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            _consumeAPIService = ActivatorUtilities.CreateInstance<ConsumeAPIService>(serviceProvider, configuration);
        }

        [TestMethod]
        public async Task GetFlightsTest()
        {
            var flights = await _consumeAPIService.GetFlights();

            Assert.IsNotNull(flights);
            Assert.IsTrue(flights.Count > 0);
        }
    }
}
