using Domain.Dto;
using Domain.Models;

namespace Bussines.Interfaces
{
    public interface IJourneyService
    {
        Task<List<Journey>> GetJourney(GetRouteDto getRoute);
        Task<List<string>> GetListCitys();
    }
}