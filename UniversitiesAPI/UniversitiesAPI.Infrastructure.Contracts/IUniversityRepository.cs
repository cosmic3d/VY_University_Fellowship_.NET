using UniversitiesAPI.Infrastructure.Contracts.APIEntities;
using UniversitiesAPI.Infrastructure.Contracts.DBEntities;

namespace UniversitiesAPI.Infrastructure.Contracts
{
    public interface IUniversityRepository
    {
        public Task<int> UniversityMigrationFromAPI2DB();

        public List<University> GetAllUniversitiesFromDB();
    }
}
