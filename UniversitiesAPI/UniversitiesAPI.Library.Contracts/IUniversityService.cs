using UniversitiesAPI.Library.Contracts.DTOs;

namespace UniversitiesAPI.Library.Contracts
{
    public interface IUniversityService
    {
        public Task<UniversityMigrationDto> ExecuteMigrationFromAPI2DB();
        public UniversityNameCountryListDto GetNameAndCountryFromUniversities();
        public UniversityNameWebListListDto GetNameAndWebListFromUniversities(string name);
    }
}
