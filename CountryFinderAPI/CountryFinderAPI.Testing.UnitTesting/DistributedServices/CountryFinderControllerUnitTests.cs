using CountryFinderAPI.Library.Contracts;
using Moq;
using CountryFinderAPI.Controllers;
using CountryFinderAPI.Library.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;
using CountryFinderAPI.XCutting.Enums;

namespace CountryFinderAPI.Testing.UnitTesting.DistributedServices
{
    public class CountryFinderControllerUnitTests
    {
        #region GetCountriesByInitialAndYear
        [Fact]
        public void GetCountriesByInitialAndYear_WhenHappyPath_ReturnsOk()
        {
            // Arrange
            Mock<ICountryFinderService> mockCountryFinderService = new();

            mockCountryFinderService
                .Setup(x => x.GetCountriesByInitialAndYear(It.IsAny<char>(), It.IsAny<int>()))
                .Returns(new CountryListDto());

            CountryFinderController sut = new CountryFinderController(mockCountryFinderService.Object);

            // Act

            ActionResult<List<CountryDto>> result = sut.GetCountriesByInitialAndYear("c", 1961);

            // Assert

            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetCountriesByInitialAndYear_WhenInvalidYear_ReturnsBadRequest()
        {
            // Arrange
            Mock<ICountryFinderService> mockCountryFinderService = new();

            mockCountryFinderService
                .Setup(x => x.GetCountriesByInitialAndYear(It.IsAny<char>(), It.IsAny<int>()))
                .Returns(new CountryListDto());

            CountryFinderController sut = new CountryFinderController(mockCountryFinderService.Object);

            // Act

            ActionResult<List<CountryDto>> result = sut.GetCountriesByInitialAndYear("c", -1995);

            // Assert

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void GetCountriesByInitialAndYear_WhenInvalidInitial_ReturnsBadRequest()
        {
            // Arrange
            Mock<ICountryFinderService> mockCountryFinderService = new();

            mockCountryFinderService
                .Setup(x => x.GetCountriesByInitialAndYear(It.IsAny<char>(), It.IsAny<int>()))
                .Returns(new CountryListDto());

            CountryFinderController sut = new CountryFinderController(mockCountryFinderService.Object);

            // Act

            ActionResult<List<CountryDto>> result = sut.GetCountriesByInitialAndYear("7", 1995);

            // Assert

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void GetCountriesByInitialAndYear_WhenUnableToReachEndpoint_ReturnsBadRequest()
        {
            // Arrange
            Mock<ICountryFinderService> mockCountryFinderService = new();

            mockCountryFinderService
                .Setup(x => x.GetCountriesByInitialAndYear(It.IsAny<char>(), It.IsAny<int>()))
                .Returns(new CountryListDto()
                {
                    HasErrors = true,
                    Error = CountryFinderErrorEnum.UnableToReachEndpoint,
                });

            CountryFinderController sut = new CountryFinderController(mockCountryFinderService.Object);

            // Act

            ActionResult<List<CountryDto>> result = sut.GetCountriesByInitialAndYear("c", -1995);

            // Assert

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void GetCountriesByInitialAndYear_WhenUnexpectedErrorOccurred_ReturnsBadRequest()
        {
            // Arrange
            Mock<ICountryFinderService> mockCountryFinderService = new();

            mockCountryFinderService
                .Setup(x => x.GetCountriesByInitialAndYear(It.IsAny<char>(), It.IsAny<int>()))
                .Returns(new CountryListDto()
                {
                    HasErrors = true,
                    Error = CountryFinderErrorEnum.UnexpectedErrorOccurred,
                });

            CountryFinderController sut = new CountryFinderController(mockCountryFinderService.Object);

            // Act

            ActionResult<List<CountryDto>> result = sut.GetCountriesByInitialAndYear("c", -1995);

            // Assert

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        #endregion
    }
}
