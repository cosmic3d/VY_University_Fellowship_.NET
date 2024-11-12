using CountryFinderAPI.Infrastructure.Contracts;
using CountryFinderAPI.Infrastructure.Contracts.Entities;
using System.Text.Json;

namespace CountryFinderAPI.Infrastructure.Impl
{
    public class CountryFinderRepository : ICountryFinderRepository
    {
        public async Task<List<Country>?> GetCountriesByLetterAndYearAsync()
        {
            using HttpClient client = new ();
            HttpResponseMessage data = await client.GetAsync("https://countriesnow.space/api/v0.1/countries/population");
            string dataAsString = await data.Content.ReadAsStringAsync();
            CountryPage? page = JsonSerializer.Deserialize<CountryPage>(dataAsString);
            if (page == null)
            {
                return null;
            }
            List<Country>? countries = page.Data.ToList();
            return countries;
        }
    }
}
