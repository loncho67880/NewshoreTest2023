using Domain.Dto;

namespace Bussines.Interfaces
{
    public interface IRoutesCalculate
    {
        List<List<FlightApiDto>> FindRoutes(List<FlightApiDto> flights, string departureStation, string arrivalStation);
    }
}
