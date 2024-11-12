using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryFinderAPI.Library.Contracts.DTOs
{
    public class CountryDto
    {
        public string? Name { get; set; }
        public int Year { get; set; }
        public long Population { get; set; }
    }
}
