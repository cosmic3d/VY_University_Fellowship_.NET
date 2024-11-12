using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CountryFinderAPI.Domain.Models
{
    public class CountryModel
    {
        public string? Name { get; set; }
        public int Year { get; set; }
        public long Population {  get; set; }
    }
}
