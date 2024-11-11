using Microsoft.AspNetCore.Mvc;
using PokeApiDDD.Library.Contracts;
using PokeApiDDD.Library.Contracts.DTOs;
using PokeApiDDD.XCutting.Enums;


namespace PokeApiDDD.DistributedServices.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokeApiController : ControllerBase
    {
        private readonly IPokemonService _pokeService;

        public PokeApiController(IPokemonService pokeService)
        {
            _pokeService = pokeService;
        }

        [HttpGet("{initial}")]
        public ActionResult CountPokemonsStartingWith(string initial)
        {
            if (initial is null || initial.Length != 1 || !Char.IsLetter(initial[0]))
            {
                return BadRequest("Incorrect syntax detected in argument. Please provide a letter");
            }
            PokemonNamesCountDto dto = _pokeService.CountNamesWithInitialC(initial.ToLower()[0]);
            if (dto.HasErrors)
            {
                switch (dto.Error)
                {
                    case PokemonNamesCountErrorEnum.PokemonApiConnectionFailed:
                        return BadRequest("An error occurred while requesting data from the PokeAPI");
                    case PokemonNamesCountErrorEnum.DatabaseConnectionFailed:
                        return BadRequest("An error occurred while connecting to the database");
                    case PokemonNamesCountErrorEnum.ErrorOccurredWhileRetrievingInfo:
                        return BadRequest("An unkown error occurred while retrieving info");
                }
            }
            return Ok(dto.Count);
        }
    }
}
