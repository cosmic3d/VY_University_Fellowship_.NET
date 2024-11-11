using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PokeApiDDD.Infrastructure.Contracts.DatabaseContext;

namespace PokeApiDDD.Infrastructure.Impl.DB
{
    public static class PokemonRepositoryDBHelper
    {
        public static void TestConnectionToDB(PokeApiContext context)
        {
            using (SqlConnection connection = new(context.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();
            }
        }
    }
}
