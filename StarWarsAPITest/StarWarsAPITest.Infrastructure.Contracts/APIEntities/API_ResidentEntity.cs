﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StarWarsAPITest.Infrastructure.Contracts.APIEntities
{

    public class API_ResidentEntity
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

}