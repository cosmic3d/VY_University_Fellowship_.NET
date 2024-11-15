using Moq;
using System;
using System.Collections.Generic;
using UniversitiesAPI.Infrastructure.Contracts;
using UniversitiesAPI.Infrastructure.Contracts.DBEntities;
using UniversitiesAPI.Library.Contracts.DTOs;
using UniversitiesAPI.Library.Impl;
using UniversitiesAPI.XCutting.Enums;

namespace UniversitiesAPI.Testing.UnitTesting.Library
{
    public class UniversityServiceUnitTests
    {
        #region ExecuteMigrationFromAPI2DB
        [Fact]
        public void ExecuteMigrationFromAPI2DB_WhenHappyPath_ReturnsDtoWithCount()
        {
            // Arrange

            Mock<IUniversityRepository> _mockUniversityRepository = new();
            _mockUniversityRepository.Setup(x => x.UniversityMigrationFromAPI2DB())
                .ReturnsAsync(3);

            UniversityService sut = new(_mockUniversityRepository.Object);

            // Act

            UniversityMigrationDto result = sut.ExecuteMigrationFromAPI2DB();

            // Assert

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Equal(3, result.MigratedUniversitiesCount);

        }

        [Fact]
        public void ExecuteMigrationFromAPI2DB_WhenHttpRequestException_ReturnsDtoWithError()
        {
            // Arrange

            Mock<IUniversityRepository> _mockUniversityRepository = new();
            _mockUniversityRepository.Setup(x => x.UniversityMigrationFromAPI2DB())
                .Throws(new HttpRequestException());

            UniversityService sut = new(_mockUniversityRepository.Object);

            // Act

            UniversityMigrationDto result = sut.ExecuteMigrationFromAPI2DB();

            // Assert

            Assert.NotNull(result);
            Assert.True(result.HasErrors);
            Assert.Equal(0, result.MigratedUniversitiesCount);
            Assert.Equal(UniversityEnumError.UnableToReachEndpoint, result.Error);

        }

        [Fact]
        public void ExecuteMigrationFromAPI2DB_WhenException_ReturnsDtoWithError()
        {
            // Arrange

            Mock<IUniversityRepository> _mockUniversityRepository = new();
            _mockUniversityRepository.Setup(x => x.UniversityMigrationFromAPI2DB())
                .ThrowsAsync(new Exception());

            UniversityService sut = new(_mockUniversityRepository.Object);

            // Act

            UniversityMigrationDto result = sut.ExecuteMigrationFromAPI2DB();

            // Assert

            Assert.NotNull(result);
            Assert.True(result.HasErrors);
            Assert.Equal(0, result.MigratedUniversitiesCount);
            Assert.Equal(UniversityEnumError.UnexpectedError, result.Error);

        }
        #endregion
        #region GetNameAndCountryFromUniversities
        [Fact]
        public void GetNameAndCountryFromUniversities_WhenHappyPath_ReturnsDtoWithUniversities()
        {
            // Arrange

            Mock<IUniversityRepository> _mockUniversityRepository = new();
            _mockUniversityRepository.Setup(x => x.GetAllUniversitiesFromDB())
                .Returns(new List<University>()
                {
                    new()
                    {
                        Name = "TestName",
                        Country = "TestCountry"
                    }
                });

            UniversityService sut = new(_mockUniversityRepository.Object);

            // Act

            UniversityNameCountryListDto result = sut.GetNameAndCountryFromUniversities();

            // Assert

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Universities);
            Assert.NotEmpty(result.Universities);
        }

        [Fact]
        public void GetNameAndCountryFromUniversities_WhenException_ReturnsDtoWithError()
        {
            // Arrange

            Mock<IUniversityRepository> _mockUniversityRepository = new();
            _mockUniversityRepository.Setup(x => x.GetAllUniversitiesFromDB())
                .Throws(new Exception());

            UniversityService sut = new(_mockUniversityRepository.Object);

            // Act

            UniversityNameCountryListDto result = sut.GetNameAndCountryFromUniversities();

            // Assert

            Assert.NotNull(result);
            Assert.True(result.HasErrors);
            Assert.Null(result.Universities);
            Assert.Equal(UniversityEnumError.UnexpectedError, result.Error);
        }
        #endregion
        #region GetNameAndWebListFromUniversities
        const string VALID_PATTERN = "TestName";
        const string INVALID_PATTERN = "    \t\t\t   \t";
        [Fact]
        public void GetNameAndWebListFromUniversities_WhenHappyPath_ReturnsDtoWithUniversities()
        {
            // Arrange

            Mock<IUniversityRepository> _mockUniversityRepository = new();
            _mockUniversityRepository.Setup(x => x.GetAllUniversitiesFromDB())
                .Returns(new List<University>()
                {
                    new()
                    {
                        Name = "TestName",
                        Country = "TestCountry",
                    }
                });

            UniversityService sut = new(_mockUniversityRepository.Object);

            // Act

            UniversityNameWebListListDto result = sut.GetNameAndWebListFromUniversities(VALID_PATTERN);

            // Assert

            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotEmpty(result.Universities);
        }

        [Fact]
        public void GetNameAndWebListFromUniversities_WhenInvalidPattern_ReturnsDtoWithError()
        {
            // Arrange

            Mock<IUniversityRepository> _mockUniversityRepository = new();
            _mockUniversityRepository.Setup(x => x.GetAllUniversitiesFromDB())
                .Returns(new List<University>()
                {
                    new()
                    {
                        Name = "TestName",
                        Country = "TestCountry",
                    }
                });

            UniversityService sut = new(_mockUniversityRepository.Object);

            // Act

            UniversityNameWebListListDto result = sut.GetNameAndWebListFromUniversities(INVALID_PATTERN);

            // Assert

            Assert.NotNull(result);
            Assert.True(result.HasErrors);
            Assert.Null(result.Universities);
            Assert.Empty(result.Universities);
            Assert.Equal(UniversityEnumError.InvalidStringParameter, result.Error);
        }

        [Fact]
        public void GetNameAndWebListFromUniversities_WhenException_ReturnsDtoWithError()
        {
            // Arrange

            Mock<IUniversityRepository> _mockUniversityRepository = new();
            _mockUniversityRepository.Setup(x => x.GetAllUniversitiesFromDB())
                .Throws(new Exception());

            UniversityService sut = new(_mockUniversityRepository.Object);

            // Act

            UniversityNameWebListListDto result = sut.GetNameAndWebListFromUniversities(VALID_PATTERN);

            // Assert

            Assert.NotNull(result);
            Assert.True(result.HasErrors);
            Assert.Null(result.Universities);
            Assert.Empty(result.Universities);
            Assert.Equal(UniversityEnumError.UnexpectedError, result.Error);
        }
        #endregion
    }
}
