using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversitiesAPI.Infrastructure.Contracts;
using UniversitiesAPI.Infrastructure.Impl;
using UniversitiesAPI.Library.Contracts;
using UniversitiesAPI.Library.Contracts.DTOs;

namespace UniversitiesAPI.DistributedServices.UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityService _service;

        public UniversityController(IUniversityService service)
        {
            _service = service;
        }
        [HttpGet("MigrateFromAPI2DB")]
        public async Task<ActionResult<UniversityMigrationDto>> MigrateFromAPI2DB()
        {
            UniversityMigrationDto response = await _service.ExecuteMigrationFromAPI2DB();
            if (response.HasErrors) return BadRequest(response.ErrorMsg);
            return Ok(response);
        }

        [HttpGet("GetNamesAndCountriesFromUniversities")]
        public ActionResult<UniversityNameCountryListDto> GetNamesAndCountriesFromUniversities()
        {
            UniversityNameCountryListDto response = _service.GetNameAndCountryFromUniversities();
            if (response.HasErrors) return BadRequest(response.ErrorMsg);
            return Ok(response);
        }

        [HttpGet("GetNamesAndWebListFromUniversities")]
        public ActionResult<UniversityNameWebListListDto> GetNamesAndWebListFromUniversities([FromQuery] string pattern)
        {
            UniversityNameWebListListDto response = _service.GetNameAndWebListFromUniversities(pattern);
            if (response.HasErrors) return BadRequest(response.ErrorMsg);
            return Ok(response);
        }
    }
}
