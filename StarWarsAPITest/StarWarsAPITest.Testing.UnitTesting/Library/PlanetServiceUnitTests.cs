using Moq;
using StarWarsAPITest.Infrastructure.Contracts;
using StarWarsAPITest.Infrastructure.Contracts.APIEntities;
using StarWarsAPITest.Library.Contracts.DTOs;
using StarWarsAPITest.Library.Impl;
using StarWarsAPITest.XCutting.Enums;

namespace StarWarsAPITest.Testing.UnitTesting.Library
{
    public class PlanetServiceUnitTests
    {
        private static readonly List<API_PlanetEntity> PLANET_LIST = new List<API_PlanetEntity>()
                {
                    new ()
                    {
                        Name = "Desengaño 21",
                        OrbitalPeriod = "TestOrbitalPeriod",
                        RotationPeriod = "TestRotationPeriod",
                        Population = "TestPopulation",
                        Climate = "TestClimate",
                        Residents = new ()
                        {
                            "Juan Cuesta",
                            "Emilio Delgado",
                            "Concha",
                        }

                    }
                };
        #region UpdateDBWithPlanetsFromAPI
        [Fact]
        public void UpdateDBWithPlanetsFromAPI_WhenHappyPath_ReturnsNoErrors()
        {
            // Arrange
            Mock<IPlanetRepository> _mockPlanetRepository = new ();
            _mockPlanetRepository.Setup(x => x.GetPlanetEntitiesFromAPI())
                .ReturnsAsync(PLANET_LIST);

            PlanetService sut = new(_mockPlanetRepository.Object);

            // Act

            PlanetListDto result = sut.UpdateDBWithPlanetsFromAPI();

            // Assert

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Planets);
            Assert.NotEmpty(result.Planets);
        }

        [Fact]
        public void UpdateDBWithPlanetsFromAPI_WhenHttpRequestException_ReturnsEndpointNotReachableError()
        {
            // Arrange
            Mock<IPlanetRepository> _mockPlanetRepository = new();
            _mockPlanetRepository.Setup(x => x.GetPlanetEntitiesFromAPI())
                .Throws(new HttpRequestException());

            PlanetService sut = new(_mockPlanetRepository.Object);

            // Act

            PlanetListDto result = sut.UpdateDBWithPlanetsFromAPI();

            // Assert

            Assert.NotNull(result);
            Assert.Null(result.Planets);
            Assert.True(result.HasErrors);
            Assert.Equal(PlanetListEnumError.UnableToReachEndpoint, result.Error);
        }

        [Fact]
        public void UpdateDBWithPlanetsFromAPI_WhenHttpRequestException_ReturnsUnexpectedError()
        {
            // Arrange
            Mock<IPlanetRepository> _mockPlanetRepository = new();
            _mockPlanetRepository.Setup(x => x.GetPlanetEntitiesFromAPI())
                .Throws(new Exception());

            PlanetService sut = new(_mockPlanetRepository.Object);

            // Act

            PlanetListDto result = sut.UpdateDBWithPlanetsFromAPI();

            // Assert

            Assert.NotNull(result);
            Assert.Null(result.Planets);
            Assert.True(result.HasErrors);
            Assert.Equal(PlanetListEnumError.UnexpectedError, result.Error);
        }
        #endregion
        #region GetPlanetResidentsFromAPIWithDBUrl
        #endregion
    }
}
