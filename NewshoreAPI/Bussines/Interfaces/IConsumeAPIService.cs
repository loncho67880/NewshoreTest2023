using Domain.Dto;

namespace Bussines.Interfaces
{
    public interface IConsumeAPIService
    {
        Task<List<FlightApiDto>> GetFlights();
    }
}
