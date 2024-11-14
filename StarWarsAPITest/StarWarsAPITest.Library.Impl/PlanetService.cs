using StarWarsAPITest.Domain.Models.Validators;
using StarWarsAPITest.Infrastructure.Contracts;
using StarWarsAPITest.Infrastructure.Contracts.APIEntities;
using StarWarsAPITest.Infrastructure.Contracts.DBEntities;
using StarWarsAPITest.Library.Contracts;
using StarWarsAPITest.Library.Contracts.DTOs;
using StarWarsAPITest.XCutting.Enums;

namespace StarWarsAPITest.Library.Impl
{
    public class PlanetService : IPlanetService
    {
        private readonly IPlanetRepository _planetRepository;
        public PlanetService(IPlanetRepository planetRepository)
        {
            _planetRepository = planetRepository;
        }

        public PlanetListDto UpdateDBWithPlanetsFromAPI()
        {
            PlanetListDto dto = new ();
            try
            {
                Task<List<API_PlanetEntity>> planetsFromAPI = _planetRepository.GetPlanetEntitiesFromAPI();
                List<DB_PlanetEntity> planetsFromDB = planetsFromAPI.Result.Select(x => new DB_PlanetEntity()
                {
                    Name = x.Name,
                    RotationPeriod = x.RotationPeriod,
                    OrbitalPeriod = x.OrbitalPeriod,
                    Climate = x.Climate,
                    Population = x.Population,
                    Url = x.Url,
                }).ToList();
                _planetRepository.UpdateDBWithPlanets(planetsFromDB);
                dto.Planets = planetsFromAPI.Result.Select (x => new PlanetDto()
                {
                    Name = x.Name
                }).ToList();
            }
            catch (Exception ex)
            {
                dto.HasErrors = true;
                if (ex is HttpRequestException)
                {
                    dto.Error = PlanetListEnumError.UnableToReachEndpoint;
                    dto.ErrorMsg = $"Unable to reach endpoint: {ex.Message}";
                }
                else
                {
                    dto.Error = PlanetListEnumError.UnexpectedError;
                    dto.ErrorMsg = $"Unexpected Error: {ex.Message}";
                }
            }
            return dto;
        }

        public ResidentListDto GetPlanetResidentsFromAPIWithDBUrl(string planetName)
        {
            ResidentListDto dto = new();
            if (!PlanetModelValidator.IsValidPlanetName(planetName))
            {
                dto.HasErrors= true;
                dto.Error = PlanetListEnumError.InvalidPlanetName;
                dto.ErrorMsg = "Invalid planet name";
            }
            else
            {
                try
                {
                    string? url = _planetRepository.GetPlanetFromDB(planetName)?.Url ?? null;
                    if (url == null)
                    {
                        dto.HasErrors = true;
                        dto.Error = PlanetListEnumError.NoPlanetsWereFound;
                        dto.ErrorMsg = $"No planets were found with name {planetName}.";
                        return dto;
                    }
                    Task<List<API_ResidentEntity>>? residentsFromAPI = _planetRepository.GetResidentEntitiesFromAPIWithDBUrl(url!);
                    if (residentsFromAPI == null)
                    {
                        dto.HasErrors = true;
                        dto.Error = PlanetListEnumError.NoResidentsWereFound;
                        dto.ErrorMsg = $"No residents were found in planet {planetName}.";
                        return dto;
                    }
                    dto.Residents = residentsFromAPI.Result.Select(x => new ResidentDto()
                    {
                        Name = x.Name
                    }).ToList();
                }
                catch (Exception ex)
                {
                    dto.HasErrors = true;
                    if (ex is HttpRequestException)
                    {
                        dto.Error = PlanetListEnumError.UnableToReachEndpoint;
                        dto.ErrorMsg = $"Unable to reach endpoint: {ex.Message}";
                    }
                    else
                    {
                        dto.Error = PlanetListEnumError.UnexpectedError;
                        dto.ErrorMsg = $"Unexpected Error: {ex.Message}";
                    }
                }
            }
            return dto;

        }
    }
}
