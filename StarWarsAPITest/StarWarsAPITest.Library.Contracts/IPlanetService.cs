
using StarWarsAPITest.Infrastructure.Contracts;
using StarWarsAPITest.Library.Contracts.DTOs;

namespace StarWarsAPITest.Library.Contracts
{
    public interface IPlanetService
    {
        public PlanetListDto UpdateDBWithPlanetsFromAPI();
        public ResidentListDto GetPlanetResidentsFromAPIWithDBUrl(string planetName);

    }
}
