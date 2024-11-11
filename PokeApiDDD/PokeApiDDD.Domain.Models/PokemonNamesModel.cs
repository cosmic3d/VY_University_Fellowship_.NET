namespace PokeApiDDD.Domain.Models
{
    public class PokemonNamesModel
    {
        public List<string> Names;

        public PokemonNamesModel(List<string> names)
        {
            Names = names;
        }

        public int CountNamesStartingWithCharC(char c)
        {
            int count = Names.Count(x => x.StartsWith(c));
            return count;
        }
    }
}
