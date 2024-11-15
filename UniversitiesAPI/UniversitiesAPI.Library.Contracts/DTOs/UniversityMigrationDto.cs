using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using UniversitiesAPI.XCutting.Enums;

namespace UniversitiesAPI.Library.Contracts.DTOs
{
    public class UniversityMigrationDto
    {
        [IgnoreDataMember] [JsonIgnore]
        public bool HasErrors { get; set; }
        [IgnoreDataMember] [JsonIgnore]
        public UniversityEnumError Error { get; set; }
        [IgnoreDataMember] [JsonIgnore]
        public string? ErrorMsg { get; set; }
        public int MigratedUniversitiesCount { get; set; }
    }
}
