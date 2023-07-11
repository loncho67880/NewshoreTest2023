using Bussines.Interfaces;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace NewshoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IJourneyService _journeyService;
        public RouteController(IJourneyService journeyService)
        {
            _journeyService = journeyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string Origin, string Destination)
        {
            var result = await _journeyService.GetJourney(new GetRouteDto()
            {
                Origin = Origin,
                Destination = Destination
            });

            if(result.Count == 0)
                return NotFound("No existe una ruta");

            return Ok(result.OrderBy(x=> x.Price));
        }
    }
}
