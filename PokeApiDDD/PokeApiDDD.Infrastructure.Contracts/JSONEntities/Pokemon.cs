using System.Text.Json.Serialization;

namespace PokeApiDDD.Infrastructure.Contracts.JSONEntities
{
    public class Pokemon
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }

}
