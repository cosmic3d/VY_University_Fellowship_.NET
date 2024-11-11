using PokeApiDDD.Library.Contracts.DTOs;

namespace PokeApiDDD.Library.Contracts
{
    public interface IPokemonService
    {
        public PokemonNamesCountDto CountNamesWithInitialC(char c);
    }
}
