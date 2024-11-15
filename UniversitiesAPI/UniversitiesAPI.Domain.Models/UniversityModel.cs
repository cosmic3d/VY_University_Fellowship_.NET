namespace UniversitiesAPI.Domain.Models
{
    public class UniversityModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string? AlphaTwoCode { get; set; }
        public List<string> Domains { get; set; }
        public List<string> WebPages { get; set; }
    }
}
