using CountryFinderAPI.Library.Contracts;
using CountryFinderAPI.Library.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;
using CountryFinderAPI.XCutting.Enums;

namespace CountryFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryFinderController : ControllerBase
    {
        private readonly ICountryFinderService _countryFinderService;
        public CountryFinderController(ICountryFinderService countryFinderService)
        {
            _countryFinderService = countryFinderService;
        }

        [HttpGet("GetCountriesByInitialAndYear")]
        public ActionResult<List<CountryDto>> GetCountriesByInitialAndYear(string initial, string year)
        {
            CountryListDto result = _countryFinderService.GetCountriesByInitialAndYear(initial!.ToUpper(), year);
            if (result.HasErrors)
            {
                switch (result.Error)
                {
                    case CountryFinderErrorEnum.UnableToReachEndpoint:
                        return BadRequest("An error occurred while trying to access the API endpoint");
                    case CountryFinderErrorEnum.IncorrectInitialFormat:
                        return BadRequest("initial must be a single character");
                    case CountryFinderErrorEnum.IncorrectYearFormat:
                        return BadRequest("year must be a value between 1961 and 2018");
                    case CountryFinderErrorEnum.UnexpectedErrorOccurred:
                        return BadRequest("An unexpected error occurred");
                }
            }
            return Ok(result);
        }
    }
}
