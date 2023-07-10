using Bussines.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NewshoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinosController : ControllerBase
    {
        private readonly IJourneyService _journeyService;
        public DestinosController(IJourneyService journeyService)
        {
            _journeyService = journeyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _journeyService.GetListCitys();

            if (result.Count == 0)
                return NotFound("No existen resultados");

            return Ok(result);
        }
    }
}
