
using AutoMapper;
using Bussines.Interfaces;
using Domain.Dto;
using Domain.Models;

namespace Bussines.Implementacion
{
    public class JourneyService : IJourneyService
    {
        private readonly IConsumeAPIService _consumeAPIService;
        private readonly IMapper _mapper;
        private readonly IRoutesCalculate _routesCalculate;

        public JourneyService(IConsumeAPIService consumeAPIService, IMapper mapper, IRoutesCalculate routesCalculate)
        {
            _consumeAPIService = consumeAPIService;
            _mapper = mapper;
            _routesCalculate = routesCalculate;
        }

        public async Task<List<Journey>> GetJourney(GetRouteDto getRoute)
        {
            var resultConsume = await _consumeAPIService.GetFlights();
            var findroute = _routesCalculate.FindRoutes(resultConsume, getRoute.Origin, getRoute.Destination);
            var result = new List<Journey>();
            foreach (List<FlightApiDto> routes in findroute)
            {
                result.Add(new Journey()
                {
                    Destination = getRoute.Destination,
                    Origin = getRoute.Origin,
                    Flights = _mapper.Map<List<Flight>>(routes),
                    Price = routes.Sum(x => x.Price)
                });
            }
            return result;
        }

        public async Task<List<string>> GetListCitys()
        {
            var resultConsume = await _consumeAPIService.GetFlights();
            List<string> result = resultConsume.Select(x => x.DepartureStation).ToList();
            result.AddRange(resultConsume.Select(x => x.ArrivalStation).ToList());
            return result.Distinct().OrderBy(x => x).ToList();
        }
    }
}