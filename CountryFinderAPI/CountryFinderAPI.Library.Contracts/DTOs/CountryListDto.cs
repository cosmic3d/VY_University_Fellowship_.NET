

using CountryFinderAPI.XCutting.Enums;

namespace CountryFinderAPI.Library.Contracts.DTOs
{
    public class CountryListDto
    {
        public bool HasErrors { get; set; }
        public CountryFinderErrorEnum Error {  get; set; }
        public List<CountryDto> countryDtos { get; set; }
    }
}
