using System.Text.Json.Serialization;

namespace UniversitiesAPI.Infrastructure.Contracts.APIEntities
{
    public class UniversityPageEntity
    {
        [JsonPropertyName("Property1")]
        public UniversityEntity[] Universities { get; set; }
    }
}
