using System;
using System.Text.Json.Serialization;

namespace StarWarsAPITest.Infrastructure.Contracts.APIEntities
{
    public class API_PlanetsPageEntity
    {
        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("results")]
        public List<API_PlanetEntity> Results { get; set; }
    }
}
