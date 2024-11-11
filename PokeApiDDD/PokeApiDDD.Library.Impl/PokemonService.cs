using Microsoft.Data.SqlClient;
using PokeApiDDD.Domain.Models;
using PokeApiDDD.Infrastructure.Contracts;
using PokeApiDDD.Library.Contracts;
using PokeApiDDD.Library.Contracts.DTOs;
using PokeApiDDD.XCutting.Enums;

namespace PokeApiDDD.Library.Impl
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepositoryJSON _jsonRepo;
        private readonly IPokemonRepositoryDB _dbRepo;
        public PokemonService(IPokemonRepositoryJSON jsonRepo, IPokemonRepositoryDB dbRepo)
        {
            _jsonRepo = jsonRepo;
            _dbRepo = dbRepo;
        }
        public PokemonNamesCountDto CountNamesWithInitialC(char c)
        {
            PokemonNamesCountDto pokemonNamesCountDto = new();
            try
            {
                Task<List<string?>?> names = _jsonRepo.GetAllPokemons();
                if (names is null || names.Result is null)
                {
                    pokemonNamesCountDto.HasErrors = true;
                    pokemonNamesCountDto.Error = PokemonNamesCountErrorEnum.ErrorOccurredWhileRetrievingInfo;
                    return pokemonNamesCountDto;
                }
                PokemonNamesModel domainModel = new(names.Result!);
                int count = domainModel.CountNamesStartingWithCharC(c);
                _dbRepo.AddInitialStatisticsRecord(c, count);
                pokemonNamesCountDto.Count = count;
                return pokemonNamesCountDto;
            }
            catch (Exception ex)
            {
                if (ex is HttpRequestException)
                {
                    //Si falla la api intentamos encontrar el último registro existente en la base de datos
                    try
                    {
                        List<string>? names = _dbRepo.GetAllPokemons();
                        if (names is null)
                        {
                            pokemonNamesCountDto.HasErrors = true;
                            pokemonNamesCountDto.Error = PokemonNamesCountErrorEnum.ErrorOccurredWhileRetrievingInfo;
                            return pokemonNamesCountDto;
                        }
                        PokemonNamesModel domainModel = new(names);
                        int count = domainModel.CountNamesStartingWithCharC(c);
                        pokemonNamesCountDto.Count = count;
                        return pokemonNamesCountDto;
                    }
                    catch (Exception ex2)
                    {
                        if (ex2 is SqlException)
                        {
                            pokemonNamesCountDto.Error = PokemonNamesCountErrorEnum.DatabaseConnectionFailed;
                            return pokemonNamesCountDto;
                        }
                        else
                        {
                            pokemonNamesCountDto.Error = PokemonNamesCountErrorEnum.ErrorOccurredWhileRetrievingInfo;
                            return pokemonNamesCountDto;
                        }
                    }

                }
                else if (ex is SqlException)
                {
                    pokemonNamesCountDto.HasErrors = true;
                    pokemonNamesCountDto.Error = PokemonNamesCountErrorEnum.DatabaseConnectionFailed;
                    return pokemonNamesCountDto;
                }
                else
                {
                    pokemonNamesCountDto.HasErrors = true;
                    pokemonNamesCountDto.Error = PokemonNamesCountErrorEnum.ErrorOccurredWhileRetrievingInfo;
                    return pokemonNamesCountDto;
                }
            }
        }
    }
}
