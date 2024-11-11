using PokeApiDDD.XCutting.Enums;

namespace PokeApiDDD.Library.Contracts.DTOs
{
    public class PokemonNamesCountDto
    {
        public bool HasErrors;
        public PokemonNamesCountErrorEnum Error;
        public int Count;
    }
}
