namespace UniversitiesAPI.Domain.Models.Validators
{
    public static class UniversityModelValidator
    {
        public static bool IsValidStringForSearch(string str)
            => str != null && str.Replace("\t", "").Trim().Length > 0;
    }
}
