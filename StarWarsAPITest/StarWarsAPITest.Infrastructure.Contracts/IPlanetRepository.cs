using StarWarsAPITest.Infrastructure.Contracts.APIEntities;
using StarWarsAPITest.Infrastructure.Contracts.DBEntities;

namespace StarWarsAPITest.Infrastructure.Contracts
{
    public interface IPlanetRepository
    {
        public Task<List<API_PlanetEntity>> GetPlanetEntitiesFromAPI();
        public Task<List<API_ResidentEntity>>? GetResidentEntitiesFromAPIWithDBUrl(string url);
        public DB_PlanetEntity? GetPlanetFromDB(string name);
        public void UpdateDBWithPlanets(List<DB_PlanetEntity> entities);
    }
}
