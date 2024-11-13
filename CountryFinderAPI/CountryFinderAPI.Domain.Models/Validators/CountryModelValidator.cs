namespace CountryFinderAPI.Domain.Models.Validators
{
    public static class CountryModelValidator
    {
        public static bool IsValidInitial(string initial)
            => initial != null &&
               initial.Length == 1 &&
               Char.IsLetter(initial[0]);
        public static bool IsValidYear(string year)
            => year != null &&
               int.TryParse(year, out int parsedYear) &&
               parsedYear >= 1961 && parsedYear <= 2018;
    }
}
