namespace PokeApiDDD.Infrastructure.Contracts
{
    public interface IPokemonRepositoryDB
    {
        public List<string>? GetAllPokemons();

        public void AddInitialStatisticsRecord(char initial, int count);
    }
}
