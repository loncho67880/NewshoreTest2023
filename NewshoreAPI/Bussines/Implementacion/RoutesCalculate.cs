using Bussines.Interfaces;
using Domain.Dto;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Bussines.Implementacion
{
    public class RoutesCalculate : IRoutesCalculate
    {
        private readonly int maxFlightApiDtos = 1;
        public RoutesCalculate(IConfiguration configuration) 
        {
            maxFlightApiDtos = Convert.ToInt16(configuration["MaxFlights"]);
        }

        public List<List<FlightApiDto>> FindRoutes(List<FlightApiDto> FlightApiDtos, string departureStation, string arrivalStation)
        {
            List<List<FlightApiDto>> routes = new List<List<FlightApiDto>>();
            List<FlightApiDto> currentRoute = new List<FlightApiDto>();
            FindRoutesRecursive(FlightApiDtos, departureStation, arrivalStation, arrivalStation, currentRoute, routes);
            return routes;
        }

        public void FindRoutesRecursive(List<FlightApiDto> FlightApiDtos, string currentStation, string arrivalStation, string arrivalStationFinal, List<FlightApiDto> currentRoute, List<List<FlightApiDto>> routes)
        {
            if (currentStation == arrivalStationFinal && currentRoute.Count <= maxFlightApiDtos)
            {
                routes.Add(new List<FlightApiDto>(currentRoute));
                return;
            }

            var listFind = FlightApiDtos.Where(f => f.DepartureStation == currentStation);
            foreach (FlightApiDto FlightApiDto in listFind)
            {
                if (!currentRoute.Contains(FlightApiDto) && !ContieneMismoVueloIdaRegreso(currentRoute, FlightApiDto.DepartureStation, FlightApiDto.ArrivalStation))
                {
                    currentRoute.Add(FlightApiDto);
                    FindRoutesRecursive(FlightApiDtos, FlightApiDto.ArrivalStation, arrivalStation, arrivalStationFinal, currentRoute, routes);
                    currentRoute.Remove(FlightApiDto);
                }
            }
        }

        private bool ContieneMismoVueloIdaRegreso(List<FlightApiDto> currentRoute, string departureStation, string arrivalStation)
        {
            return currentRoute.Any(x => x.DepartureStation == arrivalStation && x.ArrivalStation == departureStation);
        }
    }
}
