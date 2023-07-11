using Bussines.Implementacion;
using Bussines.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using UnitTest;

namespace TestProjectUnitTest.Bussines
{
    [TestClass]
    public class JourneyServiceTest : BaseTest
    {
        IJourneyService _journeyService;
        IConsumeAPIService _consumeAPIService;
        IRoutesCalculate _routesCalculate;

        [TestInitialize]
        public void InitSql()
        {
            //Arrange
            var inMemorySettings = new Dictionary<string, string> {
                {"RestFlights", "https://recruiting-api.newshore.es/"},
                {"MaxFlights", "4" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            _consumeAPIService = ActivatorUtilities.CreateInstance<ConsumeAPIService>(serviceProvider, configuration);
            _routesCalculate = ActivatorUtilities.CreateInstance<RoutesCalculate>(serviceProvider, configuration);

            _journeyService = ActivatorUtilities.CreateInstance<JourneyService>(serviceProvider, _consumeAPIService, _routesCalculate);
        }

        [TestMethod]
        public async Task GetJourneyTest()
        {
            var flights = await _journeyService.GetJourney(new Domain.Dto.GetRouteDto()
            {
                Origin = "MZL",
                Destination = "BOG"
            });

            Assert.IsNotNull(flights);
            Assert.IsTrue(flights.Count > 0);
        }

        [TestMethod]
        public async Task GetListCitysTest()
        {
            var flights = await _journeyService.GetListCitys();

            Assert.IsNotNull(flights);
            Assert.IsTrue(flights.Count > 0);
        }
    }
}
