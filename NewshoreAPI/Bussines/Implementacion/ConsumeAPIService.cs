using Bussines.Interfaces;
using Common.Consume;
using Domain.Dto;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Bussines.Implementacion
{
    public class ConsumeAPIService : IConsumeAPIService
    {
        private readonly string? baseurl;
        private readonly IMemoryCache _cache;
        private readonly RestClientInfraestructure _restClient;
        public ConsumeAPIService(IMemoryCache cache, IConfiguration configuration) 
        { 
            _cache = cache;
            baseurl = configuration["RestFlights"];
            _restClient = new RestClientInfraestructure(baseurl);
        }

        public async Task<List<FlightApiDto>> GetFlights()
        {
            return GetOrSet("GetFlights", GetFlightsApi, TimeSpan.FromDays(1));
        }

        private List<FlightApiDto> GetFlightsApi()
        {
            var resp = _restClient.GetAsync("api/flights/2").Result;
            return JsonConvert.DeserializeObject<List<FlightApiDto>>(resp);
        }

        /// <summary>
        /// Administrate Cache
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="getItemCallback"></param>
        /// <param name="cacheDuration"></param>
        /// <returns></returns>
        private TResult GetOrSet<TResult>(string cacheKey, Func<TResult> getItemCallback, TimeSpan cacheDuration)
        {
            if (!_cache.TryGetValue(cacheKey, out TResult cacheResult))
            {
                cacheResult = getItemCallback();
                _cache.Set(cacheKey, cacheResult, cacheDuration);
            }

            return cacheResult;
        }
    }
}
