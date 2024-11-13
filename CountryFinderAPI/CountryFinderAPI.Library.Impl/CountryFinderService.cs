using CountryFinderAPI.Domain.Models;
using CountryFinderAPI.Domain.Models.Validators;
using CountryFinderAPI.Infrastructure.Contracts;
using CountryFinderAPI.Infrastructure.Contracts.Entities;
using CountryFinderAPI.Library.Contracts;
using CountryFinderAPI.Library.Contracts.DTOs;
using CountryFinderAPI.XCutting.Enums;

namespace CountryFinderAPI.Library.Impl
{
    public class CountryFinderService : ICountryFinderService
    {
        private readonly ICountryFinderRepository _countryFinderRepository;

        public CountryFinderService(ICountryFinderRepository countryFinderRepository)
        {
            _countryFinderRepository = countryFinderRepository;
        }

        public CountryListDto GetCountriesByInitialAndYear(string initial, string year)
        {
            CountryListDto countryListDto = new ();
            if (!CountryModelValidator.IsValidInitial(initial))
            {
                countryListDto.HasErrors = true;
                countryListDto.Error = CountryFinderErrorEnum.IncorrectInitialFormat;
            }
            else if (!CountryModelValidator.IsValidYear(year))
            {
                countryListDto.HasErrors = true;
                countryListDto.Error = CountryFinderErrorEnum.IncorrectYearFormat;
            }
            else
            {
                try
                {
                    Task<List<Country>?> countryEntities =
                            _countryFinderRepository.GetCountriesByLetterAndYearAsync(char.Parse(initial), int.Parse(year));
                    if (countryEntities is null || countryEntities.Result is null) throw new Exception();
                    countryListDto.countryDtos = countryEntities.Result.Select(x => new CountryDto()
                    {
                        Name = x.Name,
                        Population = x.PopulationCounts?.FirstOrDefault()?.Value ?? throw new Exception(),
                    }).ToList();
                }
                catch (Exception ex)
                {
                    countryListDto.HasErrors = true;
                    if (ex is HttpRequestException)
                        countryListDto.Error = CountryFinderErrorEnum.UnableToReachEndpoint;
                    else
                        countryListDto.Error = CountryFinderErrorEnum.UnexpectedErrorOccurred;
                } 
            }
            return countryListDto;
        }
    }
}
