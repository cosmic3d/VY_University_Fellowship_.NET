using System.Text.Json.Serialization;

namespace PokeApiDDD.Infrastructure.Contracts.JSONEntities
{
    public class PokemonPage
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
        [JsonPropertyName("next")]

        public string? Next { get; set; }
        [JsonPropertyName("previous")]

        public object? Previous { get; set; }
        [JsonPropertyName("results")]

        public Pokemon[]? Results { get; set; }
    }

}