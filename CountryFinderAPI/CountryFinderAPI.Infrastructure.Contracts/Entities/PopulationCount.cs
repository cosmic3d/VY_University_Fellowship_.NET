using System.Text.Json.Serialization;

namespace CountryFinderAPI.Infrastructure.Contracts.Entities
{
    public class PopulationCount
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }
        [JsonPropertyName("value")]
        public long Value { get; set; }
    }
}
