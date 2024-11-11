using PokeApiDDD.Infrastructure.Contracts;
using PokeApiDDD.Infrastructure.Contracts.DatabaseContext;
using PokeApiDDD.Infrastructure.Contracts.Entities;

namespace PokeApiDDD.Infrastructure.Impl.DB
{
    public class PokemonRepositoryDB : IPokemonRepositoryDB
    {
        private readonly PokeApiContext _dbContext = new();

        public void AddInitialStatisticsRecord(char initial, int count)
        {
            PokemonRepositoryDBHelper.TestConnectionToDB(_dbContext);
            _dbContext.PokeStatisticsByInitial.Add(new PokeStatisticsByInitial()
            {
                Initial = initial.ToString(),
                Counter = count
            }
            );
        }

        public List<string>? GetAllPokemons()
        {
            PokemonRepositoryDBHelper.TestConnectionToDB(_dbContext);
            List<string> names = _dbContext.Pokemon.Select(x => x.Name).ToList();
            return names;
        }

    }
}
