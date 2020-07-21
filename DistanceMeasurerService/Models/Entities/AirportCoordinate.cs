using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistanceMeasurerService.Models.Entities
{
    public class AirportCoordinate
    {
        public string AirportCode { get; set; }
        public Coordinate Coordinate { get; set; }
    }
}
