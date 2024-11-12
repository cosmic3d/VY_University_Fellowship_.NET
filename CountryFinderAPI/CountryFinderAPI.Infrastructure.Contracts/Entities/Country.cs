using System.Text.Json.Serialization;

namespace CountryFinderAPI.Infrastructure.Contracts.Entities
{
    public class Country
    {
        [JsonPropertyName("country")]
        public string? Name { get; set; }
        [JsonPropertyName("code")]
        public string? Code { get; set; }
        [JsonPropertyName("iso3")]
        public string? Iso3 { get; set; }
        [JsonPropertyName("populationCounts")]
        public PopulationCount[]? PopulationCounts { get; set; }
    }
}
