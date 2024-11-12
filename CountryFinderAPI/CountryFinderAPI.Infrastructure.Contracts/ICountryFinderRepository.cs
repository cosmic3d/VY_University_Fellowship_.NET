using CountryFinderAPI.Infrastructure.Contracts.Entities;

namespace CountryFinderAPI.Infrastructure.Contracts
{
    public interface ICountryFinderRepository
    {
        public Task<List<Country>?> GetCountriesByLetterAndYearAsync();
    }
}
