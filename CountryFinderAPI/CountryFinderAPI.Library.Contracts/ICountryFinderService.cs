using CountryFinderAPI.Library.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryFinderAPI.Library.Contracts
{
    public interface ICountryFinderService
    {
        public CountryListDto GetCountriesByInitialAndYear(string c, string year);
    }
}
