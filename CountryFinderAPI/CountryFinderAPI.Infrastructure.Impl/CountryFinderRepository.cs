using CountryFinderAPI.Infrastructure.Contracts;
using CountryFinderAPI.Infrastructure.Contracts.Entities;
using System.Text.Json;

namespace CountryFinderAPI.Infrastructure.Impl
{
    public class CountryFinderRepository : ICountryFinderRepository
    {
        public string UrlCountriesPopulation = "https://countriesnow.space/api/v0.1/countries/population";
        public async Task<List<Country>?> GetCountriesByLetterAndYearAsync(char c, int year)
        {
            using HttpClient client = new ();
            HttpResponseMessage data = await client.GetAsync(UrlCountriesPopulation);
            string dataAsString = await data.Content.ReadAsStringAsync();
            CountryPage? page = JsonSerializer.Deserialize<CountryPage>(dataAsString);
            if (page == null) return null;
            else if (page.Error) throw new HttpRequestException();
            List<Country>? countries = page.Data
                .Where(x => x.Name != null &&
                            x.Name.StartsWith(c) &&
                            x.PopulationCounts?.Any(y => y.Year == year) == true)
                .Select(x => new Country
                {
                    Name = x.Name,
                    PopulationCounts = x.PopulationCounts?
                        .Where(y => y.Year == year)
                        .ToList() ?? new List<PopulationCount>()
                })
                .ToList();
            return countries;
        }
    }
}
