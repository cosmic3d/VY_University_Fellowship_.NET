using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryFinderAPI.Domain.Models
{
    public class CountryListModel
    {
        public List<CountryModel>? Countries;

        public List<CountryModel>? GetCountriesStartingByCOnYearY(char c, int year)
        {
            return Countries?.Where(x => x.Name.StartsWith(c) && x.Year == year).ToList();
        }
    }
}
