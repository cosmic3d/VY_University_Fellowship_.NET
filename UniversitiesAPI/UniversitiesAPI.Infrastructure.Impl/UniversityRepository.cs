using System.Text.Json;
using UniversitiesAPI.Infrastructure.Contracts;
using UniversitiesAPI.Infrastructure.Contracts.APIEntities;
using UniversitiesAPI.Infrastructure.Contracts.DBEntities;
using UniversitiesAPI.Infrastructure.Contracts.DBContext;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace UniversitiesAPI.Infrastructure.Impl
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly UniversityDBContext _dbContext = new();

        #region MigrationMethods
        private UniversityPage MigrateFromAPI2DBUpdate(List<UniversityEntity> universities)
        {
            UniversityPage universityPageDB = _dbContext.UniversityPage.First();
            _dbContext.Remove(universityPageDB);
            return MigrateFromAPI2DBFirstTime(universities);
        }
        private UniversityPage MigrateFromAPI2DBFirstTime(List<UniversityEntity> universities)
        {
            UniversityPage universityPageDB = new UniversityPage();
            foreach (UniversityEntity university in universities)
            {
                University universityDB = new University()
                {
                    Name = university.Name,
                    AlphaTwoCode = university.Alpha_two_code,
                    StateProvince = university.Stateprovince,
                    Country = university.Country,
                    Deleted = false,
                };
                if (university.Domains != null)
                {
                    foreach (string universityDomain in university.Domains)
                    {
                        universityDB.UniversityDomain.Add(new UniversityDomain()
                        {
                            Domain = universityDomain,
                        });
                    }
                }
                if (university.Web_pages != null)
                {
                    foreach (string universityWeb in university.Web_pages)
                    {
                        universityDB.UniversityWebPage.Add(new UniversityWebPage()
                        {
                            WebPage = universityWeb,
                        });
                    }
                }
                universityPageDB.University.Add(universityDB);
            }
            universityPageDB.Count = universityPageDB.University.Count;
            _dbContext.UniversityPage.Add(universityPageDB);
            return universityPageDB;
        }
        #endregion
        public async Task<int> UniversityMigrationFromAPI2DB()
        {
            using HttpClient client = new();
            HttpResponseMessage data = await client.GetAsync("http://universities.hipolabs.com/search");
            string dataAsString = await data.Content.ReadAsStringAsync();
            List<UniversityEntity>? universities = JsonSerializer.Deserialize<List<UniversityEntity>>(dataAsString);
            if (universities == null) throw new Exception("An error occurred while deserializing universitiesPage");

            UniversityPage universityPageDB;
            if (_dbContext.UniversityPage.IsNullOrEmpty())
            {
                universityPageDB = MigrateFromAPI2DBFirstTime(universities);
            }
            else
            {
                universityPageDB = MigrateFromAPI2DBUpdate(universities);
            }
            _dbContext.SaveChanges();
            return universityPageDB.Count;
        }

        public List<University> GetAllUniversitiesFromDB()
        {
            List<University> universities = _dbContext.University.Include(x => x.UniversityWebPage).ToList();
            return universities;
        }
    }
}
