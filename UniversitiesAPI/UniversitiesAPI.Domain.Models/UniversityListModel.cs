namespace UniversitiesAPI.Domain.Models
{
    public class UniversityListModel
    {
        public List<UniversityModel> universities { get; set; }

        public List<UniversityModel> GetUniversitiesWithNameContaining(string pattern)
        {
            return universities.FindAll(x => x.Name.ToLower().Contains(pattern.ToLower()));
        }
    }
}
