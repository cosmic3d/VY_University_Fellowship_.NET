using CountryFinderAPI.Domain.Models;
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

        public CountryListDto GetCountriesByInitialAndYear(char c, int year)
        {
            CountryListDto countryListDto = new ();
            try
            {
                Task<List<Country>?> countryEntities = _countryFinderRepository.GetCountriesByLetterAndYearAsync();
                if (countryEntities is null || countryEntities.Result is null)
                {
                    countryListDto.HasErrors = true;
                    countryListDto.Error = CountryFinderErrorEnum.UnexpectedErrorOccurred;
                    return countryListDto;
                }
                CountryListModel countryListModel = new()
                {
                    Countries = new()
                };
                foreach (Country country in countryEntities.Result)
                {
                    countryListModel.Countries.Add(
                        new CountryModel()
                        {
                            Name = country.Name,
                            Year = year,
                            Population = country.PopulationCounts?.Where(x => x.Year == year).FirstOrDefault()?.Value ?? 0,
                        });
                }
                List<CountryModel>? filteredCountryListModel = countryListModel.GetCountriesStartingByCOnYearY(c, year);
                if (filteredCountryListModel is null)
                {
                    countryListDto.HasErrors = true;
                    countryListDto.Error = CountryFinderErrorEnum.UnexpectedErrorOccurred;
                    return countryListDto;
                }
                countryListDto.countryDtos = new();
                foreach (CountryModel countryModel in filteredCountryListModel)
                {
                    countryListDto.countryDtos.Add(new()
                    {
                        Name = countryModel.Name,
                        Year = countryModel.Year,
                        Population = countryModel.Population
                    });
                }
            }
            catch (Exception ex)
            {
                countryListDto.HasErrors = true;
                if (ex is HttpRequestException)
                    countryListDto.Error = CountryFinderErrorEnum.UnableToReachEndpoint;
                else
                    countryListDto.Error = CountryFinderErrorEnum.UnexpectedErrorOccurred;
            }
            return countryListDto;
        }
    }
}
