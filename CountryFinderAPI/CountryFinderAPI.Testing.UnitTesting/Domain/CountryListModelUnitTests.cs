using CountryFinderAPI.Domain.Models;

namespace CountryFinderAPI.Testing.UnitTesting.Domain
{
    public class CountryListModelUnitTests
    {
        #region GetCountries
        [Fact]
        public void GetCountries_WhenValidCountries_ReturnsFilteredCountries()
        {
            // Arrange
            CountryListModel sut = new ();
            sut.Countries = new()
            {
                new()
                {
                    Name = "Test",
                    Year = 2004,
                    Population = 222
                },
                new()
                {
                    Name = "Test2",
                    Year = 2005,
                    Population = 333
                },
                new()
                {
                    Name = "Test3",
                    Year = 2004,
                    Population = 444
                }
            };
            // Act
            List<CountryModel>? result = sut.GetCountriesStartingByCOnYearY('T', 2004);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result!.Count());
        }
        #endregion
    }
}
