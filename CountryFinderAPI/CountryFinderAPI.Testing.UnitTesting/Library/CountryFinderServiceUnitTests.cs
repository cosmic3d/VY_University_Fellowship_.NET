using CountryFinderAPI.Infrastructure.Contracts;
using CountryFinderAPI.Infrastructure.Contracts.Entities;
using CountryFinderAPI.Library.Contracts.DTOs;
using CountryFinderAPI.Library.Impl;
using CountryFinderAPI.XCutting.Enums;
using Moq;

namespace CountryFinderAPI.Testing.UnitTesting.Library
{
    public class CountryFinderServiceUnitTests
    {
        const string VALID_YEAR = "1995";
        const string INVALID_YEAR = "-2005";

        const string VALID_INITIAL = "c";
        const string INVALID_INITIAL = "2";
        #region GetCountriesByInitialAndYear
        [Fact]
        public void GetCountriesByInitialAndYear_WhenHappyPath_ReturnsCountriesDto()
        {
            Mock<ICountryFinderRepository> mockCountryFinderRepository = new();

            List<PopulationCount> populationCounts = new()
            {
                new PopulationCount { Year = 2020, Value = 500000 },
                new PopulationCount { Year = 2021, Value = 520000 }
            };

            List<Country>? countryList = new()
            {
                new Country
                {
                    Name = "Test Country",
                    PopulationCounts = populationCounts
                },
                new Country
                {
                    Name = "Test Country2",
                    PopulationCounts = populationCounts
                }
            };
            mockCountryFinderRepository
                .Setup(x => x.GetCountriesByLetterAndYearAsync(It.IsAny<char>(), It.IsAny<int>()))
                .ReturnsAsync(countryList);

            CountryFinderService sut = new(mockCountryFinderRepository.Object);

            // Act

            CountryListDto result = sut.GetCountriesByInitialAndYear(VALID_INITIAL, VALID_YEAR);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.countryDtos);
            Assert.NotEmpty(result.countryDtos);
            Assert.Equal(2, result.countryDtos.Count);
        }

        [Fact]
        public void GetCountriesByInitialAndYear_WhenRepositoryReturnsNull_ReturnsCountriesDto()
        {
            Mock<ICountryFinderRepository> mockCountryFinderRepository = new();

            CountryFinderService sut = new(mockCountryFinderRepository.Object);

            // Act

            CountryListDto result = sut.GetCountriesByInitialAndYear(VALID_INITIAL, VALID_YEAR);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasErrors);
            Assert.Equal(CountryFinderErrorEnum.UnexpectedErrorOccurred, result.Error);
        }

        [Fact]
        public void GetCountriesByInitialAndYear_WhenHttpRequestException_ReturnsCountriesDto()
        {
            Mock<ICountryFinderRepository> mockCountryFinderRepository = new();

            mockCountryFinderRepository
                .Setup(x => x.GetCountriesByLetterAndYearAsync(It.IsAny<char>(), It.IsAny<int>()))
                .Throws(new HttpRequestException());

            CountryFinderService sut = new(mockCountryFinderRepository.Object);

            // Act

            CountryListDto result = sut.GetCountriesByInitialAndYear(VALID_INITIAL, VALID_YEAR);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasErrors);
            Assert.Equal(CountryFinderErrorEnum.UnableToReachEndpoint, result.Error);
        }

        [Fact]
        public void GetCountriesByInitialAndYear_WhenUnexpectedError_ReturnsCountriesDto()
        {
            Mock<ICountryFinderRepository> mockCountryFinderRepository = new();

            mockCountryFinderRepository
                .Setup(x => x.GetCountriesByLetterAndYearAsync(It.IsAny<char>(), It.IsAny<int>()))
                .Throws(new Exception());

            CountryFinderService sut = new(mockCountryFinderRepository.Object);

            // Act

            CountryListDto result = sut.GetCountriesByInitialAndYear(VALID_INITIAL, VALID_YEAR);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasErrors);
            Assert.Equal(CountryFinderErrorEnum.UnexpectedErrorOccurred, result.Error);
        }
        #endregion
    }
}
