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
        public ActionResult<List<CountryDto>> GetCountriesByInitialAndYear(string initial, int year)
        {
            if (initial == null || initial.Length != 1 || !Char.IsLetter(initial[0]))
            {
                return BadRequest("The initial must be a single letter");
            }
            else if (year < 1961 || year > 2018) {
                return BadRequest("The year must be between 1961 and 2018");
            }
            CountryListDto result = _countryFinderService.GetCountriesByInitialAndYear(initial!.ToUpper()[0], year);
            if (result.HasErrors)
            {
                switch (result.Error)
                {
                    case CountryFinderErrorEnum.UnableToReachEndpoint:
                        return BadRequest("An error occurred while trying to access the API endpoint");
                    case CountryFinderErrorEnum.UnexpectedErrorOccurred:
                        return BadRequest("An unexpected error occurred");
                }
            }
            return Ok(result);
        }
    }
}
