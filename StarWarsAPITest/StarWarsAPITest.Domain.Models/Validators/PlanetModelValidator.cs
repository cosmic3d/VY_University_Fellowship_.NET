using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsAPITest.Domain.Models.Validators
{
    public static class PlanetModelValidator
    {
        public static bool IsValidPlanetName(string planetName)
            => planetName != null && planetName.Length > 0;
    }
}
