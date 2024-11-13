using CountryFinderAPI.Infrastructure.Contracts.Entities;
using CountryFinderAPI.Infrastructure.Impl;

namespace CountryFinderAPI.Testing.UnitTesting.Infrastructure
{
    public class CountryFinderRepositoryUnitTests
    {
        const char VALID_INITIAL = 'A';
        const char INVALID_INITIAL = '9';

        const int VALID_YEAR = 2004;
        const int INVALID_YEAR = -1;
        #region GetCountriesByLetterAndYearAsync
        [Fact]
        public async void GetCountriesByLetterAndYearAsync_WhenEndpointError_ThrowHttpRequestException()
        {
            // Arrange
            CountryFinderRepository countryFinderRepository = new ();
            countryFinderRepository.UrlCountriesPopulation = null;
            List<Country>? result = null;
            bool exceptionThrown = false;
            // Act
            try
            {
                result = await countryFinderRepository.GetCountriesByLetterAndYearAsync(VALID_INITIAL, VALID_YEAR);
            }
            catch (Exception ex)
            {
                exceptionThrown = true;
            }
            Assert.Null(result);
            Assert.True(exceptionThrown);
        }
        #endregion
    }
}
