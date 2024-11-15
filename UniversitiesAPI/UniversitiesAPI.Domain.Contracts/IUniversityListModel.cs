using UniversitiesAPI.Domain.Models;

namespace UniversitiesAPI.Domain.Contracts
{
    public interface IUniversityListModel
    {
        public List<UniversityModel> GetUniversitiesWithNameContaining(string pattern);
    }
}
