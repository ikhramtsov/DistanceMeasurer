using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistanceMeasurerService.Models.CTeleport.Entities
{
    public partial class AirportInfo
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("city_iata")]
        public string CityIata { get; set; }

        [JsonProperty("iata")]
        public string Iata { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("timezone_region_name")]
        public string TimezoneRegionName { get; set; }

        [JsonProperty("country_iata")]
        public string CountryIata { get; set; }

        [JsonProperty("rating")]
        public long Rating { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("hubs")]
        public long Hubs { get; set; }
    }
}
