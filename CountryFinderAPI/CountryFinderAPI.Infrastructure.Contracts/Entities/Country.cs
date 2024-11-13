using System.Text.Json.Serialization;

namespace CountryFinderAPI.Infrastructure.Contracts.Entities
{
    public class Country
    {
        [JsonPropertyName("country")]
        public string? Name { get; set; }
        [JsonPropertyName("populationCounts")]
        public List<PopulationCount>? PopulationCounts { get; set; }
    }
}
