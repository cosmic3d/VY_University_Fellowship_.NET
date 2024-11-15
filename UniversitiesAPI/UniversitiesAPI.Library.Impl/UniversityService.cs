using UniversitiesAPI.Library.Contracts.DTOs;
using UniversitiesAPI.Infrastructure.Contracts;
using UniversitiesAPI.Library.Contracts;
using UniversitiesAPI.XCutting.Enums;
using UniversitiesAPI.Domain.Models.Validators;
using UniversitiesAPI.Domain.Models;

namespace UniversitiesAPI.Library.Impl
{
    public class UniversityService : IUniversityService
    {
        private readonly IUniversityRepository _repository;

        public UniversityService(IUniversityRepository repository)
        {
            _repository = repository;
        }

        public UniversityMigrationDto ExecuteMigrationFromAPI2DB()
        {
            UniversityMigrationDto dto = new();
            try
            {
                dto.MigratedUniversitiesCount = _repository.UniversityMigrationFromAPI2DB().Result;
            }
            catch (Exception ex)
            {
                dto.HasErrors = true;
                if (ex is HttpRequestException)
                {
                    dto.Error = UniversityEnumError.UnableToReachEndpoint;
                    dto.ErrorMsg = $"Unable to reach endpoint: {ex.Message}";
                }
                else
                {
                    dto.Error = UniversityEnumError.UnexpectedError;
                    dto.ErrorMsg = $"Unexpected Error: {ex.Message}";
                }
            }
            return dto;
        }

        public UniversityNameCountryListDto GetNameAndCountryFromUniversities()
        {
            UniversityNameCountryListDto dto = new();
            try
            {
                dto.Universities = _repository.GetAllUniversitiesFromDB().Select(x => new UniversityNameCountryDto()
                {
                    Name = x.Name,
                    Country = x.Country,
                }).ToList();
            }
            catch (Exception ex)
            {
                dto.HasErrors = true;
                dto.Error = UniversityEnumError.UnexpectedError;
                dto.ErrorMsg = $"Unexpected Error: {ex.Message}";
            }
            return dto;
        }

        public UniversityNameWebListListDto GetNameAndWebListFromUniversities(string name)
        {
            UniversityNameWebListListDto dto = new();
            if (!UniversityModelValidator.IsValidStringForSearch(name))
            {
                dto.HasErrors = true;
                dto.Error = UniversityEnumError.InvalidStringParameter;
                dto.ErrorMsg = $"The pattern provided is not valid to perform a search";
            }
            else
            {
                try
                {
                    UniversityListModel universityListModel = new();
                    universityListModel.universities = _repository.GetAllUniversitiesFromDB()
                        .Select(x => new UniversityModel()
                        {
                            Name = x.Name,
                            WebPages = x.UniversityWebPage.Select(y => y.WebPage).ToList(),
                        }).ToList();
                    List<UniversityModel> matchingUniversities = universityListModel.GetUniversitiesWithNameContaining(name);
                    dto.Universities = matchingUniversities.Select(x => new UniversityNameWebListDto()
                    {
                        Name = x.Name,
                        Webs = x.WebPages,
                    }).ToList();
                }
                catch (Exception ex)
                {
                    dto.HasErrors = true;
                    dto.Error = UniversityEnumError.UnexpectedError;
                    dto.ErrorMsg = $"Unexpected Error: {ex.Message}";
                }
            }
            return dto;
        }
    }
}
