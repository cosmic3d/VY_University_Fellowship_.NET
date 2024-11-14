using StarWarsAPITest.XCutting.Enums;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace StarWarsAPITest.Library.Contracts.DTOs
{
    public class ResidentListDto
    {
        [IgnoreDataMember]
        [JsonIgnore]
        public bool HasErrors { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public PlanetListEnumError Error { get; set; }
        public string? ErrorMsg { get; set; }
        public List<ResidentDto>? Residents { get; set; }
    }
}
