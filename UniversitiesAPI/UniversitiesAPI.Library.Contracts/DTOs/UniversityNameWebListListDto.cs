using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using UniversitiesAPI.XCutting.Enums;

namespace UniversitiesAPI.Library.Contracts.DTOs
{
    public class UniversityNameWebListListDto
    {
        [IgnoreDataMember] [JsonIgnore]
        public bool HasErrors { get; set; }
        [IgnoreDataMember] [JsonIgnore]
        public UniversityEnumError Error { get; set; }
        [IgnoreDataMember] [JsonIgnore]
        public string? ErrorMsg { get; set; }
        public List<UniversityNameWebListDto>? Universities { get; set; }
    }
}
