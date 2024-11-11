using PokeApiDDD.Infrastructure.Contracts;
using PokeApiDDD.Infrastructure.Contracts.JSONEntities;
using System.Text.Json;

namespace PokeApiDDD.Infrastructure.Impl.JSON
{
    public class PokemonRepositoryJSON : IPokemonRepositoryJSON
    {
        public async Task<List<string?>?> GetAllPokemons()
        {
            using HttpClient client = new();
            HttpResponseMessage data = await client.GetAsync("https://pokeapi.co/api/v2/pokemon?limit=100&offset=0");
            string dataAsString = await data.Content.ReadAsStringAsync();
            PokemonPage? pokemonPage = JsonSerializer.Deserialize<PokemonPage>(dataAsString);
            return pokemonPage?.Results?.Select(x => x.Name).ToList() ?? null;
        }
    }
}
