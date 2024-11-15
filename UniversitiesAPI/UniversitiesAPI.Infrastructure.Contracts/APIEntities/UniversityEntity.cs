using System;
using System.Text.Json.Serialization;

namespace UniversitiesAPI.Infrastructure.Contracts.APIEntities
{
    public class UniversityEntity
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("alpha_two_code")]
        public string Alpha_two_code { get; set; }
        [JsonPropertyName("domains")]
        public string[] Domains { get; set; }
        [JsonPropertyName("stateprovince")]
        public string Stateprovince { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("web_pages")]
        public string[] Web_pages { get; set; }
    }

}
