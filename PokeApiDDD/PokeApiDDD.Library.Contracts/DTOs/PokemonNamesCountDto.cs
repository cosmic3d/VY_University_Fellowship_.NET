using PokeApiDDD.XCutting.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PokeApiDDD.Library.Contracts.DTOs
{
    public class PokemonNamesCountDto
    {
        [IgnoreDataMember]
        public bool HasErrors;
        [IgnoreDataMember]
        public PokemonNamesCountErrorEnum Error;
        [Range(0, 9999999)]
        public int Count = 0;
    }
}
