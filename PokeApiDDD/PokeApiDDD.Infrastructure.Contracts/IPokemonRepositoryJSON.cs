namespace PokeApiDDD.Infrastructure.Contracts
{
    public interface IPokemonRepositoryJSON
    {
        public Task<List<string?>?> GetAllPokemons();
    }
}
