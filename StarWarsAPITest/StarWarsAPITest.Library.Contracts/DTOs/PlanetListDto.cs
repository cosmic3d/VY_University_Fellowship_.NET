using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using StarWarsAPITest.XCutting.Enums;

namespace StarWarsAPITest.Library.Contracts.DTOs
{
    public class PlanetListDto
    {
        [IgnoreDataMember] [JsonIgnore]
        public bool HasErrors { get; set; }
        [IgnoreDataMember] [JsonIgnore]
        public PlanetListEnumError Error { get; set; }
        public string? ErrorMsg { get; set; }
        public List<PlanetDto>? Planets { get; set; }
    }
}
