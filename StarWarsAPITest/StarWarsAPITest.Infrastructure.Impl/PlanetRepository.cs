using Microsoft.EntityFrameworkCore;
using StarWarsAPITest.Infrastructure.Contracts;
using StarWarsAPITest.Infrastructure.Contracts.APIEntities;
using StarWarsAPITest.Infrastructure.Contracts.DBEntities;
using StarWarsAPITest.Infrastructure.Contracts.SWDBContext;
using System.Text.Json;

namespace StarWarsAPITest.Infrastructure.Impl
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly SWDBContext _dbContext = new();
        public async Task<List<API_PlanetEntity>> GetPlanetEntitiesFromAPI()
        {
            using HttpClient client = new ();
            HttpResponseMessage data = await client.GetAsync("https://swapi.dev/api/planets/?format=json");
            string dataAsString = await data.Content.ReadAsStringAsync();
            API_PlanetsPageEntity? planetsPage = JsonSerializer.Deserialize<API_PlanetsPageEntity>(dataAsString);
            List<API_PlanetEntity>? planets = planetsPage?.Results ?? throw new Exception("An error occurred while deserializing planetsPage");
            return planets;
        }


        public async Task<List<API_ResidentEntity>>? GetResidentEntitiesFromAPIWithDBUrl(string url)
        {
            if (url == null)
                throw new ArgumentNullException("Name cannot be null");
            using HttpClient client = new();
            HttpResponseMessage data = await client.GetAsync(url);
            string dataAsString = await data.Content.ReadAsStringAsync();
            API_PlanetEntity? planet = JsonSerializer.Deserialize<API_PlanetEntity>(dataAsString);
            if (planet == null) throw new Exception("An error occurred while deserializing planet");
            if (planet.Residents == null || !planet.Residents.Any()) return null;
            //cambiar para que planet se consiga desde base de datos
            List <API_ResidentEntity> residents = new();
            foreach (string residentUrl in planet.Residents)
            {
                data = await client.GetAsync(residentUrl);
                dataAsString = await data.Content.ReadAsStringAsync();
                API_ResidentEntity? resident = JsonSerializer.Deserialize<API_ResidentEntity>(dataAsString);
                if (resident == null) throw new Exception("An error occurred while deserializing planet");
                residents.Add(resident);
            }
            return residents;
        }
        public DB_PlanetEntity? GetPlanetFromDB(string name) =>
            _dbContext.Planets.Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();

        public void UpdateDBWithPlanets(List<DB_PlanetEntity> entities)
        {
            foreach (var entity in entities)
            {
                // Busca el planeta en la base de datos usando el campo URL como identificador único
                var existingPlanet = _dbContext.Planets.SingleOrDefault(p => p.Url == entity.Url);

                if (existingPlanet != null)
                {
                    existingPlanet.Name = entity.Name;
                    existingPlanet.Climate = entity.Climate;
                    existingPlanet.RotationPeriod = entity.RotationPeriod;
                    existingPlanet.OrbitalPeriod = entity.OrbitalPeriod;
                    existingPlanet.Population = entity.Population;
                }
                else
                {
                    _dbContext.Planets.Add(entity);
                }
            }
            _dbContext.SaveChanges();
        }
    }
}
