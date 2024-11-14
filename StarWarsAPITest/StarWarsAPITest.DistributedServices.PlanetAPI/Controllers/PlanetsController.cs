using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarWarsAPITest.Library.Contracts;
using StarWarsAPITest.Library.Contracts.DTOs;
using StarWarsAPITest.Library.Impl;

namespace StarWarsAPITest.DistributedServices.PlanetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        private readonly IPlanetService _planetService;
        public PlanetsController(IPlanetService planetService)
        {
            _planetService = planetService;
        }

        [HttpGet("UpdatePlanets")]
        public ActionResult<PlanetListDto> UpdatePlanetsFromDB()
        {
            PlanetListDto response = _planetService.UpdateDBWithPlanetsFromAPI();
            if (response.HasErrors) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("GetResidents")]
        public ActionResult<ResidentListDto> GetResidentsFromPlanet([FromQuery] string planet)
        {
            ResidentListDto response = _planetService.GetPlanetResidentsFromAPIWithDBUrl(planet);
            if (response.HasErrors) return BadRequest(response);
            return Ok(response);
        }
    }
}
