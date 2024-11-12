using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CountryFinderAPI.Infrastructure.Contracts.Entities
{

    public class CountryPage
    {
        [JsonPropertyName("error")]
        public bool Error { get; set; }
        [JsonPropertyName("msg")]
        public string Msg { get; set; }
        [JsonPropertyName("data")]
        public Country[] Data { get; set; }
    }
}
